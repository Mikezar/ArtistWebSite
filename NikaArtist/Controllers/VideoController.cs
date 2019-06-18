using NikaArtist.Service.LoggerService;
using NikaArtist.Service.Services;
using System.Web.Mvc;

namespace NikaArtist.Controllers
{
    public class VideoController : BaseController
    {
		private readonly IVideoService _videoService;

		public VideoController(ILoggerProvider loggerProvider, IVideoService videoService) : base(loggerProvider)
		{
			_videoService = videoService;
		}

		public ActionResult Index()
        {
			var videos = _videoService.GetVideos();

			return View(videos);
        }
    }
}