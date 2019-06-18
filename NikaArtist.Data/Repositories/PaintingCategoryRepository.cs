using LinqToDB;
using NikaArtist.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikaArtist.Data.Repositories
{
    public sealed class PaintingCategoryRepository : IPaintingCategoryRepository
    {
        public async Task AddCategory(PaintingCategories category)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                await dataConnection.InsertAsync(category);
            }
        }

        public IEnumerable<PaintingCategories> GetAllCategories(int paintingId)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                return dataConnection.PaintingCategories.Where(x => x.PaintingId == paintingId).ToList();
            }
        }

		public IEnumerable<PaintingCategories> GetAllPaintings(int categoryId)
		{
			using (var dataConnection = new DatabaseConnection())
			{
				return dataConnection.PaintingCategories.Where(x => x.CategoryId == categoryId).ToList();
			}
		}

		public async Task RemoveCategories(int paintingId)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                await dataConnection.PaintingCategories.Where(x => x.PaintingId == paintingId).DeleteAsync();
            }
        }
    }
}
