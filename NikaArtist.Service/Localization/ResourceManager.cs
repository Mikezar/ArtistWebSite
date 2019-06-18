using NikaArtist.Service.FileHandler;
using System;
using System.Runtime.Caching;
using System.Web;

namespace NikaArtist.Service.Localization
{
    public class ResourceManager : IResourceManager
    {
        private static string ResourcePath = HttpContext.Current.Server.MapPath("/SysData/Resources/");
        private static ObjectCache Cache => MemoryCache.Default;
        private CacheItemPolicy _policy = new CacheItemPolicy()
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30)
        };

        private IResource SetCache(string resourceName, IResource resource)
        {
            Cache.Set(resourceName, resource, _policy);
            return resource;
        }

        public IResource GetResource(string resourceName, string locale)
        {
            if (string.IsNullOrEmpty(resourceName)) throw new ArgumentNullException(nameof(resourceName));
            if (string.IsNullOrEmpty(locale)) throw new ArgumentNullException(nameof(locale));

            var resource = (IResource)Cache.Get(resourceName);

            if(resource == null)
            {
                var fileName = $"{locale}.{resourceName}.json";
                var file = FileLoader.Load(ResourcePath + fileName);

                return SetCache($"{locale}.{resourceName}", new Resource(file, locale));
            }

            return resource;
        }
	}
}
