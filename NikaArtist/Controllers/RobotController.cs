using NikaArtist.Service.LoggerService;
using NikaArtist.Service.Utils;
using System.Web.Mvc;

namespace NikaArtist.Controllers
{
	public class RobotController : BaseController
	{
		public RobotController(ILoggerProvider loggerProvider) : base(loggerProvider) { }

		public ContentResult Index()
		{
			string path = HttpContext.Server.MapPath("~/robots.txt");

			string content = ContentLoader.Get(path);

			return Content(content, "text/plain");
		}
	}
}