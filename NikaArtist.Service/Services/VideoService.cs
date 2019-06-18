using System;
using System.Linq;
using System.Threading.Tasks;
using NikaArtist.Data.Entities;
using NikaArtist.Data.Repositories;
using NikaArtist.Service.Models;

namespace NikaArtist.Service.Services
{
    public class VideoService : IVideoService
    {
        private readonly IRepository<Video> _videoRepository;
        private readonly ICategoryService _categoryService;
		private readonly IPaintingService _paintingService;

		public VideoService(IRepository<Video> videoRepository, ICategoryService categoryService, IPaintingService paintingService)
        {
            _videoRepository = videoRepository;
            _categoryService = categoryService;
			_paintingService = paintingService;
		}

		private PaintingModel GetPaintingByPath(string coverPath)
		{
			return _paintingService.GetPaintings().Paintings.FirstOrDefault(x => x.Path == coverPath);
		}

        public async Task AddAsync(VideoEditModel model)
        {
            await _videoRepository.AddAsync(new Video()
            {
                Id = model.Id,
                TitleEn = model.TitleEng,
                TitleRu = model.TitleRu,
                DescriptionEn = model.DescriptionEng,
                DescriptionRu = model.DescriptionRu,
                CategoryId = model.CategoryId,
                Created = model.CreationDate,
				CoverId = model.CoverId.Value,
                Path = model.Path
            });
        }

        public async Task UpdateAsync(VideoEditModel model)
        {
            var video = GetVideo(model.Id);

            if (video == null)
            {
                await AddAsync(model);
                return;
            }

            video.Path = model.Path;
			video.CoverId = model.CoverId.Value;
            video.TitleEn = model.TitleEng;
            video.TitleRu = model.TitleRu;
            video.DescriptionEn = model.DescriptionEng;
            video.DescriptionRu = model.DescriptionRu;
            video.CategoryId = model.CategoryId;

            await _videoRepository.UpdateAsync(video);
        }

        public async Task DeleteAsync(int id)
        {
            await _videoRepository.RemoveAsync(id);
        }

        public VideoEditModel GetEditModel(int id)
        {
            var video = GetVideo(id);

            if(video == null)
            {
                return new VideoEditModel()
                {
                    CreationDate = DateTimeOffset.Now,
                    Categories = _categoryService.BuildCategories()
                };
            }
            else
            {
                return new VideoEditModel()
                {
                    Id = video.Id,
                    CategoryId = video.CategoryId,
                    CreationDate = video.Created,
                    DescriptionEng = video.DescriptionEn,
                    DescriptionRu = video.DescriptionRu,
                    Path = video.Path,
					CoverId = video.CoverId,
                    TitleEng = video.TitleEn,
                    TitleRu = video.TitleRu,
                    Categories = _categoryService.BuildCategories()
                };
            }
        }

        public Video GetVideo(int id)
        {
            return _videoRepository.Get(id);
        }

        public VideoModelList GetVideos()
        {
            return new VideoModelList()
            {
                Videos = _videoRepository.GetAll(50, 0).Select(x => new VideoModel()
                {
                    Id = x.Id,
                    CategoryId = x.CategoryId,
                    DescriptionEn = x.DescriptionEn,
                    DescriptionRu = x.DescriptionRu,
                    TitleRu = x.TitleRu,
                    TitleEn = x.TitleEn,
                    Created = x.Created,
                    Path = x.Path,
					CoverId = x.CoverId,
					CoverPath = _paintingService.GetPainting(x.CoverId)?.ThumbPath
				}).ToList()
            };
        }
    }
}
