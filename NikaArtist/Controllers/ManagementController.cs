using NikaArtist.Infrastructure;
using NikaArtist.Service.LoggerService;
using NikaArtist.Service.Models;
using NikaArtist.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NikaArtist.Controllers
{
    [Auth]
    [Authorize(Users = "guest")]
    public class ManagementController : BaseController
    {
        private readonly IPaintingService _paintingService;
        private readonly ICategoryService _categoryService;
        private readonly IVideoService _videoService;
        private readonly IArticleService _articleService;

        public ManagementController(ILoggerProvider loggerProvider, 
            IPaintingService paintingService, 
            IVideoService videoService,
            ICategoryService categoryService,
            IArticleService articleService) : base(loggerProvider)
        {
            _paintingService = paintingService;
            _videoService = videoService;
            _categoryService = categoryService;
            _articleService = articleService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Paintings()
        {
            var model = _paintingService.GetPaintings();

            return View(new PaintingFilterModel()
			{
				PaintingModel = model,
				Categories = _categoryService.BuildCategories()
			});
        }

        [HttpGet]
        public ActionResult Videos()
        {
            var model = _videoService.GetVideos();

            return View(model);
        }


        [HttpGet]
        public ActionResult Articles()
        {
            var model = _articleService.GetArticles();
            return View(model);
        }


        #region Статьи


        [HttpGet]
        public ActionResult EditArticle(int id = 0)
        {
            var model = _articleService.GetEditModel(id);

            return PartialView("~/Views/Management/Modals/_EditArticle.cshtml", model);
        }

        [HttpPost]
        public async Task<ActionResult> EditArticle(ArticleEditModel model)
        {
            try
            {
				if (string.IsNullOrEmpty(model.RawHtmlEn) ||
				    string.IsNullOrEmpty(model.RawHtmlEn))
						throw new InvalidOperationException("Не заполнено тело статьи");
					
                await _articleService.UpdateAsync(model);
                return GetSuccessResponse();
            }
            catch (Exception exception)
            {
                return GetErrorResponse(exception);
            }
        }

		[HttpPost]
		public async Task<ActionResult> DeleteArticle(int id)
		{
			try
			{
				await _articleService.DeleteAsync(id);
				return GetSuccessResponse();
			}
			catch (Exception exception)
			{
				return GetErrorResponse(exception);
			}
		}


		#endregion



		#region Работа с видео



		[HttpGet]
        public ActionResult EditVideo(int id = 0)
        {
            var model = _videoService.GetEditModel(id);

            return PartialView("~/Views/Management/Modals/_EditVideo.cshtml", model);
        }

        [HttpPost]
        public async Task<ActionResult> EditVideo(VideoEditModel model)
        {
            try
            {
                await _videoService.UpdateAsync(model);
                return GetSuccessResponse();
            }
            catch (Exception exception)
            {
                return GetErrorResponse(exception);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteVideo(int id)
        {
            try
            {
                await _videoService.DeleteAsync(id);

                return GetSuccessResponse();
            }
            catch (Exception exception)
            {
                return GetErrorResponse(exception);
            }
        }



        #endregion



        #region Обработка изображений

		private PaintingModelList _GetPaintings(int? id)
		{
			PaintingModelList model = null;

			if (id.HasValue)
			{
				model = _paintingService.GetPaintingsByCategory(id.Value, 100, 0);
			}
			else
			{
				model = _paintingService.GetPaintings();
			}

			return model;
		}

		public ActionResult GetPaintings(int? id)
        {
			var model = _GetPaintings(id);

			return PartialView("~/Views/Management/_PaintingList.cshtml", model);
        }

		public ActionResult GetPaintingsSelectList(int? id)
		{
			var model = _GetPaintings(id);

			return PartialView("~/Views/Management/Modals/_PaintingSelectModal.cshtml", model);
		}

		public ActionResult EditPainting(int id, string returnUrl = null)
        {
            var model = _paintingService.GetEditModel(id);

            return PartialView("~/Views/Management/Modals/_EditPainting.cshtml", model);
        }

        [HttpPost]
        public async Task<ActionResult> EditPainting(PaintingUploadModel model)
        {
            try
            {
                await _paintingService.UpdateAsync(model);
                return GetSuccessResponse();
            }
            catch (Exception exception)
            {
                return GetErrorResponse(exception);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeletePainting(int id)
        {
            try
            {
                await _paintingService.DeleteAsync(id);

                return GetSuccessResponse();
            }
            catch (Exception exception)
            {
                return GetErrorResponse(exception);
            }
        }



        #endregion



        #region Загрузка и обработка изображения

        [HttpGet]
        public ActionResult UploadPainting()
        {
            var model = Session["Uploads"] as UploadModel ?? new UploadModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> SaveUploadPhoto()
        {
            var model = Session["Uploads"] as UploadModel;

            try
            {
                await _paintingService.AddManyAsync(model.Uploads.ToArray());

                Session["Uploads"] = null;

                return RedirectToAction("Paintings");
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
                return PartialView("~/Views/Management/_UploadedPaintings.cshtml", model);
            }
        }

		[HttpPost]
		public async Task<JsonResult> ChangeOrder(ImageSortModel model)
		{
			try
			{
				await _paintingService.ChangeOrderAsync(model);

				return GetSuccessResponse();
			}
			catch (Exception exception)
			{
				Logger.LogError(exception);
				return GetErrorResponse(exception);
			}
		}

        public ActionResult EditUploadPhoto(int id)
        {
            var model = Session["Uploads"] as UploadModel;

            var photo = model.Uploads.Single(x => x.Id == id);

            photo.Action = "EditUploadPhoto";
            photo.Categories = _categoryService.BuildCategories();
            

            return PartialView("~/Views/Management/Modals/_EditPainting.cshtml", photo);
        }

        [HttpPost]
        public ActionResult EditUploadPhoto(PaintingUploadModel model)
        {
            try
            {
                var list = Session["Uploads"] as UploadModel;

                for (int i = 0; i < list.Uploads.Count; i++)
                    if (list.Uploads[i].Id == model.Id) list.Uploads[i] = model;

                Session["Uploads"] = list;
            }
            catch (Exception exception)
            {
               Logger.LogError(exception);
            }

            return RedirectToAction("UploadPainting");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UploadPainting(IEnumerable<HttpPostedFileBase> files, UploadModel uploadModel)
        {
            var model = _paintingService.Upload(files, uploadModel);
            return PartialView(model);
        }

        public ActionResult CancelUpload()
        {
            try
            {
                var list = Session["Uploads"] as UploadModel;

                foreach (var photo in list.Uploads)
                {
                    System.IO.File.Delete(Server.MapPath(photo.PhotoPath));
                    System.IO.File.Delete(Server.MapPath(photo.ThumbnailPath));
                }

                Session["Uploads"] = null;
            }
            catch (Exception exception)
            {
                Logger.LogError(exception);
            }

            return RedirectToAction("Paintings");
        }

        [HttpPost]
        public ActionResult DeleteUploadPhoto(int id)
        {
            try
            {
                var model = Session["Uploads"] as UploadModel;

                var photo = model.Uploads.Single(x => x.Id == id);

                System.IO.File.Delete(Server.MapPath(photo.PhotoPath));
                System.IO.File.Delete(Server.MapPath(photo.ThumbnailPath));
                model.Uploads.Remove(photo);

                return GetSuccessResponse();
            }
            catch (Exception exception)
            {
                return GetErrorResponse(exception);
            }
        }

        #endregion
    }
}