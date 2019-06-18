using System;

namespace NikaArtist.Service.Models
{
    public class VideoModel
    {
        public int Id { get; set; }
        public string TitleRu { get; set; }
        public string DescriptionRu { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionEn { get; set; }
        public string Path { get; set; }
		public int CoverId { get; set; }
		public string CoverPath { get; set; }
		public int CategoryId { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
