using NikaArtist.Service.LoggerService;
using NikaArtist.Infrastructure.ViewModels;
using System.Web.Mvc;
using System.Web.Security;

namespace NikaArtist.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(ILoggerProvider loggerProvider) : base(loggerProvider)
        {
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginModel()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (string.IsNullOrEmpty(model.Login)) ModelState.AddModelError("Login", "Поле логина не может быть пустым");
            if (string.IsNullOrEmpty(model.Password)) ModelState.AddModelError("Password", "Поле пароля не может быть пустым");

            if (!ModelState.IsValid) return View(model);

            FormsAuthentication.SignOut();
            if (FormsAuthentication.Authenticate(model.Login, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Login, false);

                if (!string.IsNullOrEmpty(model.ReturnUrl)) return Redirect(model.ReturnUrl);
               
            }
            else
            {
                model.Error = "Не верный пароль или логин.";
                return View(model);
            }

            return RedirectToAction("Index", "Management");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}