using System;
using System.Linq;
using System.Threading.Tasks;
using NikaArtist.Data.Entities;
using NikaArtist.Data.Repositories;
using NikaArtist.Service.Models;

namespace NikaArtist.Service.Services
{
    public sealed class ArticleService : IArticleService
    {
        private readonly IRepository<Article> _articleRepository;
		private readonly IRepository<Painting> _paintingRepository;

		public ArticleService(IRepository<Article> articleRepository, IRepository<Painting> paintingRepository)
        {
            _articleRepository = articleRepository;
			_paintingRepository = paintingRepository;
		}

        public Article GetArticle(int id)
        {
            return _articleRepository.Get(id);
        }

        public ArticleModelList GetArticles()
        {
            return new ArticleModelList()
            {
                Articles = _articleRepository.GetAll(100, 0).Select(x => new ArticleModel()
                {
                    Id = x.Id,
					CoverId = x.CoverId,
                    ArticleTitleEn = x.ArticleTitleEn,
                    ArticleTitleRu = x.ArticleTitleRu,
					DescriptionRu = x.DescriptionRu,
					DescriptionEn = x.DescriptionEn,
                    CreationDate = x.CreationDate,
                    RawHtmlEn = x.RawHtmlEn,
                    RawHtmlRu = x.RawHtmlRu
                })
            };
        }

        public ArticleEditModel GetEditModel(int id)
        {
            var article = GetArticle(id);


            if (article == null)
            {
                return new ArticleEditModel()
                {
                    CreationDate = DateTimeOffset.Now
                };
            }
            else
            {
                return new ArticleEditModel()
                {
                    Id = article.Id,
                    CreationDate = article.CreationDate,
                    ArticleTitleRu = article.ArticleTitleRu,
                    ArticleTitleEn = article.ArticleTitleEn,
					DescriptionRu = article.DescriptionRu,
					DescriptionEn = article.DescriptionEn,
					RawHtmlEn = article.RawHtmlEn,
                    RawHtmlRu = article.RawHtmlRu,
					CoverId = article.CoverId
                };
            }
        }

        public async Task AddAsync(ArticleEditModel model)
        {
            await _articleRepository.AddAsync(new Article()
            {
                Id = model.Id,
                ArticleTitleEn = model.ArticleTitleEn,
                ArticleTitleRu = model.ArticleTitleRu,
				DescriptionRu = model.DescriptionRu,
				DescriptionEn = model.DescriptionEn,
				RawHtmlEn = model.RawHtmlEn,
                RawHtmlRu = model.RawHtmlRu,
                CreationDate = model.CreationDate,
				CoverId = model.CoverId
			});
        }

        public async Task UpdateAsync(ArticleEditModel model)
        {
            var article = GetArticle(model.Id);

            if (article == null)
            {
                await AddAsync(model);
                return;
            }

            article.ArticleTitleEn = model.ArticleTitleEn;
            article.ArticleTitleRu = model.ArticleTitleRu;
			article.DescriptionEn = model.DescriptionEn;
			article.DescriptionRu = model.DescriptionRu;
			article.RawHtmlEn = model.RawHtmlEn;
            article.RawHtmlRu = model.RawHtmlRu;
			article.CoverId = model.CoverId;

            await _articleRepository.UpdateAsync(article);
        }

		public ArticleViewModelList GetArticlesViewModel()
		{
			return new ArticleViewModelList()
			{
				Articles = _articleRepository.GetAll(100, 0).Select(x => new ArticleViewModel()
				{
					Id = x.Id,
					Cover = x.CoverId.HasValue ? _paintingRepository.Get(x.CoverId.Value)  : null,
					ArticleTitleEn = x.ArticleTitleEn,
					ArticleTitleRu = x.ArticleTitleRu,
					DescriptionEn = x.DescriptionEn,
					DescriptionRu = x.DescriptionRu,
					CreationDate = x.CreationDate,
					RawHtmlEn = x.RawHtmlEn,
					RawHtmlRu = x.RawHtmlRu
				})
			};
		}

		public ArticleViewModel GetArticleViewModel(int id)
		{
			var article = _articleRepository.Get(id);

			return new ArticleViewModel()
			{
				Id = article.Id,
				Cover = article.CoverId.HasValue ? _paintingRepository.Get(article.CoverId.Value) : null,
				ArticleTitleEn = article.ArticleTitleEn,
				ArticleTitleRu = article.ArticleTitleRu,
				DescriptionEn = article.DescriptionEn,
				DescriptionRu = article.DescriptionRu,
				CreationDate = article.CreationDate,
				RawHtmlEn = article.RawHtmlEn,
				RawHtmlRu = article.RawHtmlRu
			};
		}

		public async Task DeleteAsync(int id)
		{
			await _articleRepository.RemoveAsync(id);
		}
	}
}
