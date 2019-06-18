using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NikaArtist.Data.Entities;
using NikaArtist.Data.Repositories;
using NikaArtist.Service.Converters;
using NikaArtist.Service.FileHandler;
using NikaArtist.Service.Models;

namespace NikaArtist.Service.Services
{
    public sealed class PaintingService : IPaintingService
    {
        private readonly IRepository<Painting> _paintigRepository;
        private readonly ICategoryService _categoryService;
        private readonly IImageProcessor _imageProcessor;
        private readonly IPaintingCategoryRepository _paintingCategoryRepository;

        public PaintingService(
            IRepository<Painting> paintigRepository, 
            IImageProcessor imageProcessor, 
            IRepository<Category> categoryRepository,
            IPaintingCategoryRepository paintingCategoryRepository,
            ICategoryService categoryService)
        {
            _paintigRepository = paintigRepository;
            _imageProcessor = imageProcessor;
            _paintingCategoryRepository = paintingCategoryRepository;
            _categoryService = categoryService;
        }

        public Painting GetPainting(int id)
        {
            return _paintigRepository.Get(id);
        }

        public PaintingUploadModel GetEditModel(int id)
        {
            var photo = GetPainting(id);
            var category = _paintingCategoryRepository.GetAllCategories(id).FirstOrDefault();

            var model = new PaintingUploadModel()
            {
                Id = photo.Id,
				Order = photo.Order,
                PhotoPath = photo.Path,
                ThumbnailPath = photo.ThumbPath,
                CreationDate = photo.Created,
                FileName = photo.FileName,
                TitleRu = photo.TitleRu,
                TitleEng = photo.TitleEn,
                DescriptionEng = photo.DescriptionEn,
                DescriptionRu = photo.DescriptionRu,
                Action = "EditPainting",
                Categories = _categoryService.BuildCategories(),
                CategoryId = category?.CategoryId ?? default(int)
            };

            return model;
        }

        public PaintingModelList GetPaintings()
        {
            return new PaintingModelList()
            {
                Paintings = _paintigRepository.GetAll(100, 0)
					.Select(x => x.Convert()).OrderBy(x => x.Order).ToList()
            };
        }

		public PaintingModelList GetPaintingsByCategory(int id, int take, int skip)
		{
			var paintings = _paintingCategoryRepository.GetAllPaintings(id).Select(x => x.PaintingId).ToList();

			return new PaintingModelList()
			{
				Paintings = _paintigRepository.GetAll(take, skip)
					.Where(x => paintings.Contains(x.Id))
					.Select(x => x.Convert()).OrderBy(x => x.Order).ToList()
			};
		}

		public async Task DeleteAsync(int id)
        {
            var painting = GetPainting(id);

            await _paintingCategoryRepository.RemoveCategories(id);
            await _paintigRepository.RemoveAsync(id);

            File.Delete(HttpContext.Current?.Server?.MapPath(painting?.Path));
            File.Delete(HttpContext.Current?.Server?.MapPath(painting?.ThumbPath));
        }

        public async Task AddManyAsync(IEnumerable<PaintingUploadModel> models)
        {
            models.ToList().ForEach(async (x) =>
            {
                var painting = await _paintigRepository.AddAsync(new Painting()
                {
					Id = x.Id,
					Order = x.Order,
					Created = x.CreationDate,
                    DescriptionEn = x.DescriptionEng,
                    DescriptionRu = x.DescriptionRu,
                    TitleEn = x.TitleEng,
                    TitleRu = x.TitleRu,
                    Path = x.PhotoPath,
                    ThumbPath = x.ThumbnailPath,
                    FileName = x.FileName
                });

				if (x.CategoryId != default(int))
				{
					await _paintingCategoryRepository.AddCategory(new PaintingCategories()
					{
						PaintingId = painting.Id,
						CategoryId = x.CategoryId
					});
				}
                
            });

            await Task.CompletedTask;
        }

		public async Task UpdateAsync(PaintingUploadModel model)
        {
            var painting = GetPainting(model.Id);

            painting.TitleEn = model.TitleEng;
            painting.TitleRu = model.TitleRu;
            painting.DescriptionEn = model.DescriptionEng;
            painting.DescriptionRu = model.DescriptionRu;
			painting.Order = model.Order;

			await _paintigRepository.UpdateAsync(painting);

            if (model.CategoryId != default(int))
            {
                await _paintingCategoryRepository.RemoveCategories(painting.Id);
                await _paintingCategoryRepository.AddCategory(new PaintingCategories()
                {
                    PaintingId = painting.Id,
                    CategoryId = model.CategoryId
                });
            }
        }

		public async Task ChangeOrderAsync(ImageSortModel model)
		{
			var currentPainting = GetPainting(model.CurrentId);
			var swappedPainting = GetPainting(model.SwappedId);

			currentPainting.Order = currentPainting.Order == default(int) ? model.CurrentId : currentPainting.Order;
			swappedPainting.Order = swappedPainting.Order == default(int) ? model.SwappedId : swappedPainting.Order;

			int temp = currentPainting.Order;
			currentPainting.Order = swappedPainting.Order;
			swappedPainting.Order = temp;

			await _paintigRepository.UpdateAsync(currentPainting);
			await _paintigRepository.UpdateAsync(swappedPainting);
		}

		public UploadModel Upload(IEnumerable<HttpPostedFileBase> files, UploadModel listModel)
        {
            var model = HttpContext.Current.Session["Uploads"] != null ? HttpContext.Current.Session["Uploads"] as UploadModel : new UploadModel();

            int maxId = 0;
			int order = 0;

			var paintings = GetPaintings().Paintings;

            // Если в БД уже есть фотографии, значит получаем макс. Id фотографии
            if (paintings.Any())
            {
                maxId = paintings.Max(x => x.Id);
				order = paintings.Max(x => x.Order);
			}

            // Проверяем, имеются ли в сессии уже загруженные фотогарфии, если да, то берем за макс Id значение из сессии
            if (model.Uploads.Any())
            {
                maxId = model.Uploads.Max(x => x.Id);
				order = model.Uploads.Max(x => x.Order);
			}

            foreach (var file in files)
            {
                var filename = $"photo_Ph-S{++maxId}";

                var photoUploadModel = new PaintingUploadModel()
                {
                    Id = maxId,
                    FileName = filename,
                    PhotoPath = AppDefaults.PhotoPath + filename + Path.GetExtension(file.FileName).ToLower(),
                    ThumbnailPath = AppDefaults.ThumbPath + filename + "s" + Path.GetExtension(file.FileName).ToLower(),
                    CreationDate = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Now),
					Order = ++order
				};

                if (file.ContentLength < 4048576)
                {
                    if (file != null)
                    {
                        _imageProcessor.CreateThumbnail(file, 350, 350, filename);
                        file.SaveAs(HttpContext.Current.Server.MapPath(photoUploadModel.PhotoPath));
                        model.Uploads.Add(photoUploadModel);
                        HttpContext.Current.Session["Uploads"] = model;
                    }
                }
            }

            return model;
        }
	}
}
