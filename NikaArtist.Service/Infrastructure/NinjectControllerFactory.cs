using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NikaArtist.Service.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly INinjectResolver _ninjectResolver;
        
        private NinjectControllerFactory(INinjectResolver ninjectResolver)
        {
            _ninjectResolver = ninjectResolver;
        }

        public static NinjectControllerFactory Create(Func<INinjectResolver> resolverFunc)
        {
            return new NinjectControllerFactory(resolverFunc());
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            var controllerInstance = controllerType == null ? null : (IController)_ninjectResolver.Resolve(controllerType);

            if (controllerInstance == null) throw new HttpException(404, "Bad request");

            return controllerInstance;
        }

        public override void ReleaseController(IController controller)
        {
            _ninjectResolver.Release(controller);
        }
    }
}
