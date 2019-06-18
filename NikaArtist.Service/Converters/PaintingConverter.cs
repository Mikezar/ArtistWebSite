using NikaArtist.Data.Entities;
using NikaArtist.Service.Models;

namespace NikaArtist.Service.Converters
{
	public static class PaintingConverter
	{
		public static PaintingModel Convert(this Painting painting)
		{
			return new PaintingModel()
			{
				Id = painting.Id,
				Created = painting.Created,
				DescriptionEn = painting.DescriptionEn,
				DescriptionRu = painting.DescriptionRu,
				TitleRu = painting.TitleRu,
				TitleEn = painting.TitleEn,
				Path = painting.Path,
				ThumbPath = painting.ThumbPath,
				Order = painting.Order
			};
		}
	}
}
