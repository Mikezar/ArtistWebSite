using Newtonsoft.Json;
using System.Collections.Generic;

namespace NikaArtist.Service.Localization
{
    public class Resource : IResource
    {
        private readonly IDictionary<string, string> _resource;
        
        public string Locale { get; }

        public Resource(string rawData, string locale)
        {
            _resource = JsonConvert.DeserializeObject<IDictionary<string, string>>(rawData);
            Locale = locale;
        }

        public string GetTranslation(string title)
        {
            if (string.IsNullOrEmpty(title)) return null;

            if(_resource.ContainsKey(title))
            {
                return _resource[title];
            }

            return null;
        }
    }
}
