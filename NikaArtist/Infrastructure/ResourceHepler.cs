using NikaArtist.Service.Localization;
using System.Threading;

namespace NikaArtist.Infrastructure
{
	public static class ResourceHepler
	{
		private static IResourceManager _resourceManager;

		public static IResourceManager GetManager()
		{
			return _resourceManager ?? (_resourceManager = new ResourceManager());
		}

		public static string GetTranslation(string resourceName, string key)
		{
			var resource = GetManager().GetResource(resourceName, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower());

			return resource?.GetTranslation(key);
		}
	}
}