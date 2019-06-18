using NikaArtist.Data.Entities;
using NikaArtist.Service.Models;
using System.Threading.Tasks;

namespace NikaArtist.Service.Services
{
    public interface IArticleService
    {
        Article GetArticle(int id);
        ArticleModelList GetArticles();
        ArticleEditModel GetEditModel(int id);
        Task AddAsync(ArticleEditModel model);
        Task UpdateAsync(ArticleEditModel model);
		Task DeleteAsync(int id);

		ArticleViewModelList GetArticlesViewModel();
		ArticleViewModel GetArticleViewModel(int id);
	}
}
