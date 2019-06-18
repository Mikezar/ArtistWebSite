using LinqToDB;
using LinqToDB.Data;
using NikaArtist.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikaArtist.Data.Repositories
{
    public class PaintingRepository : IRepository<Painting>
    {
        public Painting Get(int id)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                return dataConnection.Paintings.FirstOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<Painting> GetAll(int offset, int skip)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                return dataConnection.Paintings.Skip(skip).Take(offset).ToList();
            }
        }

        public async Task<Painting> AddAsync(Painting entity)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                entity.Id = await dataConnection.InsertWithInt32IdentityAsync(entity);

                return entity;
            }
        }

        public async Task<Painting> UpdateAsync(Painting entity)
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
                await dataConnection.Paintings.Where(x => x.Id == id).DeleteAsync();
            }
        }

        public async Task RemoveAsync(int[] ids)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                await dataConnection.Paintings.Where(x => ids.Contains(x.Id)).DeleteAsync();
            }
        }
    }
}
