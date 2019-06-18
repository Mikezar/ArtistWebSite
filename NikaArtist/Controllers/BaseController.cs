using NikaArtist.Service.LoggerService;
using NLog;
using System;
using System.Web.Mvc;

namespace NikaArtist.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ILogger<Logger> Logger;

		public BaseController(ILoggerProvider loggerProvider)
        {
            Logger = loggerProvider.GetLogger<Logger>();
		}

        protected JsonResult GetSuccessResponse()
        {
            return Json(
                new { success = true }
            );
        }

        protected JsonResult GetErrorResponse(Exception exception)
        {
            Logger.LogError(exception);

            return Json(
                new { success = false, errorMessage = exception.Message }
            );
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Logger.LogError(filterContext.Exception);

            filterContext.ExceptionHandled = true;

            filterContext.Result = new ViewResult()
            {
                ViewName = "Error"
            };
        }
    }
}