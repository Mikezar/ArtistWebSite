using NikaArtist.Service.Models;
using NikaArtist.Service.Utils;
using System;
using System.Web.Mvc;

namespace NikaArtist.Infrastructure
{
	public static class HtmlHelperExt
	{
		public static MvcHtmlString BuildCaption(PaintingModel painting)
		{
			return new MvcHtmlString(CultureHelper.IsEnCulture ?
				String.Concat(painting.TitleEn, " ", painting.DescriptionEn) :
				String.Concat(painting.TitleRu, " ", painting.DescriptionRu));
		}

		public static MvcHtmlString BuildCaption(VideoModel video)
		{
			return new MvcHtmlString(CultureHelper.IsEnCulture ?
				String.Concat(video.TitleEn, " ", video.DescriptionEn) :
				String.Concat(video.TitleRu, " ", video.DescriptionRu));
		}
	}
}