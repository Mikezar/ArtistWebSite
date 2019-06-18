using NikaArtist.Service.LoggerService;
using NLog;
using System.Web;
using System.Web.Routing;

namespace NikaArtist.Service.Infrastructure.ImageHandling
{
	public class ImageRouteHandler : IRouteHandler
	{
		private readonly string _photoPath;
		private readonly string _thumbPath;

		public ImageRouteHandler(string photoPath, string thumbPath)
		{
			_photoPath = photoPath;
			_thumbPath = thumbPath;
		}

		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			var logger = new NLogLoggerProvider().GetLogger<Logger>();
			return new FileHandler(_photoPath, _thumbPath, requestContext, logger);
		}
	}

	public class ImageRouteHandlerConstaint : IRouteConstraint
	{
		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			if (routeDirection == RouteDirection.UrlGeneration)
				return false;
			if (values.ContainsKey("controller") || values.ContainsKey("action"))
				return false;

			if (httpContext.Request.FilePath.Contains("image"))
				return true;
			else
				return false;
		}
	}
}
