using Ninject;
using System;

namespace NikaArtist.Service.Infrastructure
{
    public interface INinjectResolver
    {
        IKernel Kernel { get; }
        INinjectResolver Register<TService, TImplementation>() where TImplementation : TService;
        TService Resolve<TService>();
        object Resolve(Type type);
        void Release(object obj);
    }
}
