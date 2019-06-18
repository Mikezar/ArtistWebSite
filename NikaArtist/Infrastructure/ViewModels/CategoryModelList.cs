using NikaArtist.Service.Models;
using System.Collections.Generic;

namespace NikaArtist.Infrastructure.ViewModels
{
	public class CategoryModelList
	{
		public IEnumerable<CategoryModel> Categories { get; set; }
	}
}