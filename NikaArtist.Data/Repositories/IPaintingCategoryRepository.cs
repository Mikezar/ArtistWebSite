using NikaArtist.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NikaArtist.Data.Repositories
{
    public interface IPaintingCategoryRepository
    {
        IEnumerable<PaintingCategories> GetAllCategories(int paintingId);
		IEnumerable<PaintingCategories> GetAllPaintings(int categoryId);
		Task AddCategory(PaintingCategories category);
        Task RemoveCategories(int paintingId);
    }
}
