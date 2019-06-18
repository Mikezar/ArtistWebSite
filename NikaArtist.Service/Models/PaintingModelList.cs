using System.Collections.Generic;

namespace NikaArtist.Service.Models
{
    public class PaintingModelList
    {
        public IEnumerable<PaintingModel> Paintings { get; set; } = new List<PaintingModel>();
    }
}
