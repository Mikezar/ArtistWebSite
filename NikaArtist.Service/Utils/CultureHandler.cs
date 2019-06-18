using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace NikaArtist.Service.Utils
{
    public class CultureHandler : ICultureHandler
	{
        private readonly string[] _supportedLanguages;

        private void SetCurrentThreadCulture(string value)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(value);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(value);
        }

        private void AddCookie(HttpContext httpContext, string value)
        {
            httpContext.Response.Cookies.Add(new HttpCookie("CurrentUICulture") { Value = value });
        }

        public CultureHandler(string[] supportedLanguages)
        {
            _supportedLanguages = supportedLanguages;
        }

		public void SetCulture(HttpContext httpContext, string locale)
		{
			SetCurrentThreadCulture(locale);
			AddCookie(httpContext, locale);
		}

		public void SetCulture(HttpContext httpContext)
        {
            HttpCookie cookie = httpContext.Request.Cookies["CurrentUICulture"];

            if (cookie?.Value != null)
            {
                SetCurrentThreadCulture(cookie.Value);
            }
            else
            {
                var languages = httpContext?.Request?.UserLanguages;

                string language = "ru-RU";

                if (languages == null && languages.Any())
                {
                    for (int i = 0; i < languages.Length; i++)
                    {
                        if (_supportedLanguages.Contains(languages[i]))
                        {
                            language = languages[i];
                            break;
                        }
                    }
                }

				SetCulture(httpContext, language);
            }
        }
    }
}
