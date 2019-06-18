using NikaArtist.Data.Entities;
using System;

namespace NikaArtist.Service.Models
{
	public class ArticleViewModel
	{
		public int Id { get; set; }

		public string ArticleTitleRu { get; set; }

		public string ArticleTitleEn { get; set; }

		public string DescriptionRu { get; set; }

		public string DescriptionEn { get; set; }

		public string RawHtmlRu { get; set; }

		public string RawHtmlEn { get; set; }

		public DateTimeOffset CreationDate { get; set; }

		public Painting Cover { get; set; }
	}
}
