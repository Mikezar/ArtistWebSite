using System.Web.Mvc;
using NikaArtist.Service.LoggerService;
using NikaArtist.Service.Utils;

namespace NikaArtist.Controllers
{
    public class HomeController : BaseController
    {
		private readonly ICultureHandler _cultureHandler;

        public HomeController(ILoggerProvider loggerProvider, ICultureHandler cultureHandler) : base(loggerProvider)
        {
			_cultureHandler = cultureHandler;
		}

        public ActionResult Index()
        {
                return View();
        }    

		public ActionResult ChangeLanguage(string locale, string returnUrl)
		{
			_cultureHandler.SetCulture(System.Web.HttpContext.Current, locale);

			if(!string.IsNullOrEmpty(returnUrl))
			{
				return Redirect(returnUrl);
			}

			return RedirectToAction("Index");
		}
    }
}