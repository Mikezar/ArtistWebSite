using System;
using Ninject;

namespace NikaArtist.Service.Infrastructure
{
    public class NinjectResolver : INinjectResolver
    {
        private readonly IKernel _kernel;

        public NinjectResolver()
        {
            _kernel = new StandardKernel();
        }

        public IKernel Kernel => _kernel;

        public INinjectResolver Register<TService, TImplementation>() where TImplementation : TService
        {
            _kernel.Bind<TService>().To<TImplementation>();

            return this;
        }

        public void Release(object obj)
        {
            _kernel.Release(obj);
        }

        public TService Resolve<TService>()
        {
            return (TService)_kernel.Get(typeof(TService));
        }

        public object Resolve(Type type)
        {
            return _kernel.Get(type);
        }
    }
}
