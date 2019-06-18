using System.Linq;
using System.Web.Mvc;
using NikaArtist.Infrastructure.ViewModels;
using NikaArtist.Service.LoggerService;

namespace NikaArtist.Controllers
{
    public class ErrorController : BaseController
    {
		private bool IfAnyArrors()
		{
			if (HttpContext.AllErrors == null) return false;

			return HttpContext.AllErrors.Any();
		}

        public ErrorController(ILoggerProvider loggerProvider) : base(loggerProvider)
        {
        }

        public ActionResult Index(int statusCode)
        {
			if (!IfAnyArrors()) return RedirectToAction("Index", "Home", null);

            Response.ContentType = "text/html";
            return View("Error", new ErrorModel() { StatusCode = statusCode });
        }
    }
}