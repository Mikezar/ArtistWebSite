using NikaArtist.Controllers;
using NikaArtist.Data.Entities;
using NikaArtist.Data.Repositories;
using NikaArtist.Properties;
using NikaArtist.Service;
using NikaArtist.Service.FileHandler;
using NikaArtist.Service.Infrastructure;
using NikaArtist.Service.Localization;
using NikaArtist.Service.LoggerService;
using NikaArtist.Service.Services;
using NikaArtist.Service.Utils;
using NLog;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NikaArtist
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly CultureHandler _cultureHandler = new CultureHandler(new[] { "en-US", "ru-RU" });
        private static INinjectResolver _ninject { get; set; }

        private INinjectResolver ConfigureResolver()
        {
            _ninject = new NinjectResolver()
                .Register<ILoggerProvider, NLogLoggerProvider>()
                .Register<IResourceManager, ResourceManager>()
                .Register<IPaintingService, PaintingService>()
                .Register<IVideoService, VideoService>()
                .Register<ICategoryService, CategoryService>()
                .Register<IRepository<Painting>, PaintingRepository>()
                .Register<IRepository<Category>, CategoryRepository>()
                .Register<IPaintingCategoryRepository, PaintingCategoryRepository>()
                .Register<IRepository<Article>, ArticleRepository>()
                .Register<IArticleService, ArticleService>()
                .Register<IRepository<Video>, VideoRepository>()
				.Register<ICultureHandler, CultureHandler>();

            _ninject.Kernel.Bind<IImageProcessor>().To<ImageProcessor>()
                .WithConstructorArgument("path", AppDefaults.PhotoPath)
                .WithConstructorArgument("thumbFolder", AppDefaults.ThumbPath);

			_ninject.Kernel.Bind<IMail>().To<Mail>()
			   .WithConstructorArgument("email", Settings.Default.Email)
			   .WithConstructorArgument("password", Settings.Default.Password);

			return _ninject;
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ControllerBuilder.Current.SetControllerFactory(NinjectControllerFactory.Create(ConfigureResolver));
        }

        protected void Application_BeginRequest(object sender, EventArgs _args)
        {
            _cultureHandler.SetCulture(HttpContext.Current);
        }

        protected void Application_Error()
        {
            var logProvider = _ninject.Resolve<NLogLoggerProvider>();

            Exception exception = Server.GetLastError();
            HttpException httpException = exception as HttpException;

            if (httpException == null)
            {
                httpException = new HttpException(500, "Internal Server Error", exception);
                logProvider.GetLogger<Logger>().LogError(exception);
            }
            Response.Clear();

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
			routeData.Values.Add("action", "Index");
			routeData.Values.Add("statusCode", httpException.GetHttpCode());
			routeData.Values.Add("fromAppErrorEvent", true);
            Server.ClearError();

            IController controller = new ErrorController(logProvider);
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }
    }
}