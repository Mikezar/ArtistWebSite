using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NikaArtist.Service.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<SelectListItem> InsertEmptyFirst(this IEnumerable<SelectListItem> list, string emptyText = "", string emptyValue = "")
        {
            return new[] { new SelectListItem { Text = emptyText, Value = emptyValue } }.Concat(list);
        }
      
    }
}
