using System;
using System.IO;
using System.Threading.Tasks;

namespace NikaArtist.Service.FileHandler
{
    public class FileLoader
    {
        public static async Task<string> LoadAsync(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

            using (var sr = new StreamReader(path))
            {
                return await sr.ReadToEndAsync();
            }
        }

        public static string Load(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

            using (var sr = new StreamReader(path))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
