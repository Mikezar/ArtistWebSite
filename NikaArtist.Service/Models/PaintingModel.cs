using System;

namespace NikaArtist.Service.Models
{
    public class PaintingModel
    {
        public int Id { get; set; }
        public string TitleRu { get; set; }
        public string DescriptionRu { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionEn { get; set; }
        public string Path { get; set; }
        public string ThumbPath { get; set; }
        public DateTimeOffset Created { get; set; }
		public int Order { get; set; }
	}
}
