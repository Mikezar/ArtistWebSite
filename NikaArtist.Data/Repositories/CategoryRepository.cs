using NikaArtist.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikaArtist.Data.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        public Category Get(int id)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                return dataConnection.Categories.FirstOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<Category> GetAll(int offset, int skip)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                return dataConnection.Categories.Skip(skip).Take(offset).ToList();
            }
        }

        public Task<Category> AddAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
