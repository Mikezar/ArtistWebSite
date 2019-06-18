using NikaArtist.Infrastructure;
using NikaArtist.Properties;
using NikaArtist.Service.Infrastructure.ImageHandling;
using System.Web.Mvc;
using System.Web.Routing;

namespace NikaArtist
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute("RobotsTxtRoute", "robots.txt", new { controller = "Robot", action = "Index" });

			//Обработка изображений
			routes.Add(ImageRoute.Create(Settings.Default.PhotoPath, Settings.Default.ThumbPath));

			routes.MapRoute(
				name: "Art",
				url: "Gallery/{action}/{categoryId}",
				defaults: new { controller = "Gallery", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
