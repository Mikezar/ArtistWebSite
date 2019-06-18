using System.Web;

namespace NikaArtist.Service.Utils
{
	public interface ICultureHandler
	{
		void SetCulture(HttpContext httpContext, string locale);
		void SetCulture(HttpContext httpContext);
	}
}
