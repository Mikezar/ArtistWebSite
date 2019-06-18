using NikaArtist.Data.Entities;
using NikaArtist.Data.Repositories;
using NikaArtist.Service.Extensions;
using NikaArtist.Service.Models;
using NikaArtist.Service.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NikaArtist.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

		public CategoryModel GetCategory(int id)
		{
			var entity =  _categoryRepository.Get(id);

			if (entity == null) return null;

			return new CategoryModel()
			{
				Id = entity.Id,
				Title = CultureHelper.IsEnCulture ? entity.CategoryTitleEn : entity.CategoryTitleRu,
				ImagePath = entity.ImagePath
			};
		}

		public IEnumerable<SelectListItem> BuildCategories()
        {
            return _categoryRepository.GetAll(20, 0).GetSelectListItem().InsertEmptyFirst("None", "0");
        }

		public IEnumerable<CategoryModel> GetChildCategories()
		{
			return _categoryRepository.GetAll(20, 0).Where(x => x.ParentId != null).Select(x => new CategoryModel() {

				Id = x.Id,
				Title = CultureHelper.IsEnCulture ? x.CategoryTitleEn : x.CategoryTitleRu,
				ImagePath = x.ImagePath
			});
		}
	}
}
