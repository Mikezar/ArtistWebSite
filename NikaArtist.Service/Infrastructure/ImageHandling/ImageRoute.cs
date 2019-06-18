using System.Web.Routing;

namespace NikaArtist.Service.Infrastructure.ImageHandling
{
	public static class ImageRoute
	{
		public static Route Create(string photoPath, string thumbPath)
		{
			return new Route(
				"image",
				null,
				new RouteValueDictionary(new { MvcContraint = new ImageRouteHandlerConstaint() }),
				new ImageRouteHandler(photoPath, thumbPath));
		}
	}
}