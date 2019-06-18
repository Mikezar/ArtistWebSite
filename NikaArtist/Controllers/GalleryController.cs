using System.Web.Mvc;
using NikaArtist.Infrastructure.ViewModels;
using NikaArtist.Service.LoggerService;
using NikaArtist.Service.Models;
using NikaArtist.Service.Services;

namespace NikaArtist.Controllers
{
    public class GalleryController : BaseController
    {
		private readonly ICategoryService _categoryService;
		private readonly IPaintingService _paintingService;

		public GalleryController(ILoggerProvider loggerProvider, ICategoryService categoryService, IPaintingService paintingService) : base(loggerProvider)
        {
			_categoryService = categoryService;
			_paintingService = paintingService;
		}

        public ActionResult Index()
        {
			var model = new CategoryModelList()
			{
				Categories = _categoryService.GetChildCategories()
			};

            return View(model);
        }

		public ActionResult Art(int categoryId, int take = 25, int skip = 0)
		{
			var category = _categoryService.GetCategory(categoryId);

			var model = new GalleryPaintingModel()
			{
				Category = category ?? new CategoryModel(),
				Paintings = category != null ? _paintingService.GetPaintingsByCategory(category.Id, take, skip) : new PaintingModelList()
			};

			return View(model);
		}
    }
}