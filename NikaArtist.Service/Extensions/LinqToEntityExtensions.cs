using NikaArtist.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NikaArtist.Service.Extensions
{
    public static class LinqToEntityExtensions
    {
        public static IEnumerable<SelectListItem> GetSelectListItem(this IEnumerable<Category> categories)
        {
            return categories.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.CategoryTitleRu,
            }).ToList();
        }
    }
}
