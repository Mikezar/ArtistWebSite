using LinqToDB;
using NikaArtist.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikaArtist.Data.Repositories
{
    public sealed class ArticleRepository : IRepository<Article>
    {
        public Article Get(int id)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                return dataConnection.Articles.FirstOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<Article> GetAll(int offset, int skip)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                return dataConnection.Articles.Skip(skip).Take(offset).ToList();
            }
        }

        public async Task<Article> AddAsync(Article entity)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                entity.Id = await dataConnection.InsertWithInt32IdentityAsync(entity);

                return entity;
            }
        }

        public async Task<Article> UpdateAsync(Article entity)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                await dataConnection.UpdateAsync(entity);

                return entity;
            }
        }

        public async Task RemoveAsync(int id)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                await dataConnection.Articles.Where(x => x.Id == id).DeleteAsync();
            }
        }

        public async Task RemoveAsync(int[] ids)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                await dataConnection.Articles.Where(x => ids.Contains(x.Id)).DeleteAsync();
            }
        }
    }
}
