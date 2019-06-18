using LinqToDB.Mapping;
using System;

namespace NikaArtist.Data.Entities
{
    [Table(Name = "Videos")]
    public class Video
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column(Name = "TitleRu")]
        public string TitleRu { get; set; }

        [Column(Name = "DescriptionRu")]
        public string DescriptionRu { get; set; }

        [Column(Name = "TitleEn")]
        public string TitleEn { get; set; }

        [Column(Name = "DescriptionEn")]
        public string DescriptionEn { get; set; }

        [Column(Name = "CategoryId"), NotNull]
        public int CategoryId { get; set; }

        [Column(Name = "Path"), NotNull]
        public string Path { get; set; }

		[Column(Name = "CoverId"), NotNull]
		public int CoverId { get; set; }

		[Column(Name = "Created"), NotNull]
        public DateTimeOffset Created { get; set; }
    }
}
