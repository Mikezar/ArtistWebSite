using System.Collections.Generic;

namespace NikaArtist.Service.Models
{
	public class ArticleViewModelList
	{
		public IEnumerable<ArticleViewModel> Articles { get; set; } = new List<ArticleViewModel>();		
	}
}
