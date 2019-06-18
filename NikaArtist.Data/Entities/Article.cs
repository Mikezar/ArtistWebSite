using LinqToDB.Mapping;
using System;

namespace NikaArtist.Data.Entities
{
    [Table(Name = "Articles")]
    public class Article
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

		[Column(Name = "CoverId"), Nullable]
		public int? CoverId { get; set; }

        [Column(Name = "ArticleTitleRu"), NotNull]
        public string ArticleTitleRu { get; set; }

        [Column(Name = "ArticleTitleEn"), NotNull]
        public string ArticleTitleEn { get; set; }

		[Column(Name = "DescriptionRu"), Nullable]
		public string DescriptionRu { get; set; }

		[Column(Name = "DescriptionEn"), Nullable]
		public string DescriptionEn { get; set; }

		[Column(Name = "RawHtmlRu"), NotNull]
        public string RawHtmlRu { get; set; }

        [Column(Name = "RawHtmlEn"), NotNull]
        public string RawHtmlEn { get; set; }

        [Column(Name = "CreationDate"), NotNull]
        public DateTimeOffset CreationDate { get; set; }
    }
}
