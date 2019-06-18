using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NikaArtist.Service.LoggerService;
using NikaArtist.Service.Services;

namespace NikaArtist.Controllers
{
    public class BlogController : BaseController
    {
		private readonly IArticleService _articleService;

        public BlogController(ILoggerProvider loggerProvider, IArticleService articleService) : base(loggerProvider)
        {
			_articleService = articleService;

		}

        public ActionResult Index()
        {
			var models = _articleService.GetArticlesViewModel();

			return View(models);
        }

		public ActionResult Article(int id)
		{
			var model = _articleService.GetArticleViewModel(id);

			return View(model);
		}
	}
}