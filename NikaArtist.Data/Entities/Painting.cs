using LinqToDB.Mapping;
using System;

namespace NikaArtist.Data.Entities
{
    [Table(Name = "Paintings")]
    public class Painting
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Column(Name = "TitleRu")]
        public string TitleRu { get; set; }

        [Column(Name = "DescriptionRu")]
        public string DescriptionRu { get; set; }

        [Column(Name = "TitleEn")]
        public string TitleEn { get; set; }

        [Column(Name = "DescriptionEn")]
        public string DescriptionEn { get; set; }

        [Column(Name = "Path"), NotNull]
        public string Path { get; set; }

        [Column(Name = "ThumbPath"), NotNull]
        public string ThumbPath { get; set; }

        [Column(Name = "FileName"), NotNull]
        public string FileName { get; set; }

        [Column(Name = "Created"), NotNull]
        public DateTimeOffset Created { get; set; }

		[Column(Name = "Order"), NotNull]
		public int Order { get; set; }
    }
}
