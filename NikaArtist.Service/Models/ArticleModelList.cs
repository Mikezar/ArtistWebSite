using System.Collections.Generic;

namespace NikaArtist.Service.Models
{
    public class ArticleModelList
    {
        public IEnumerable<ArticleModel> Articles { get; set; } = new List<ArticleModel>();
    }
}
