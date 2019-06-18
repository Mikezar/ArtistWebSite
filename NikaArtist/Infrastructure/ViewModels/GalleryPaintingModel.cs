using NikaArtist.Service.Models;

namespace NikaArtist.Infrastructure.ViewModels
{
	public class GalleryPaintingModel
	{
		public CategoryModel Category { get; set; }
		public PaintingModelList Paintings { get; set; }
	}
}