using System.Collections.Generic;

namespace NikaArtist.Service.Models
{
    public class VideoModelList
    {
        public IEnumerable<VideoModel> Videos { get; set; } = new List<VideoModel>();
    }
}
