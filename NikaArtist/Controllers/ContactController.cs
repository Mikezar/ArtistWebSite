using NikaArtist.Infrastructure.ViewModels;
using NikaArtist.Service.LoggerService;
using NikaArtist.Service.Utils;
using reCAPTCHA.MVC;
using System.Security;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NikaArtist.Controllers
{
	public class ContactController : BaseController
	{
		private readonly IMail _mailClient;

		public ContactController(ILoggerProvider loggerProvider, IMail mailClient) : base(loggerProvider)
		{
			_mailClient = mailClient;
		}

		public ActionResult Index()
		{
			return View(new ContactSendModel());
		}

		[HttpPost]
		[CaptchaValidator]
		public async Task<JsonResult> Send(ContactSendModel model, bool captchaValid)
		{
			if (!captchaValid) return GetErrorResponse(new SecurityException());

			Logger.InternalLogger.Info($"{model.Email}-{model.Message}");

			var message = _mailClient.FormFeedBackMessage(model.Email, model.Subject, model.Message);
			await _mailClient.SendAsync(message);

			return GetSuccessResponse();
		}
	}
}