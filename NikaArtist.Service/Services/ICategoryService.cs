using NikaArtist.Service.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NikaArtist.Service.Services
{
    public interface ICategoryService
    {
		CategoryModel GetCategory(int id);
		IEnumerable<SelectListItem> BuildCategories();
		IEnumerable<CategoryModel> GetChildCategories();
	}
}
