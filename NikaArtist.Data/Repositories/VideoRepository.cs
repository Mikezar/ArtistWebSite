using LinqToDB;
using NikaArtist.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikaArtist.Data.Repositories
{
    public sealed class VideoRepository : IRepository<Video>
    {
        public Video Get(int id)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                return dataConnection.Videos.FirstOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<Video> GetAll(int offset, int skip)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                return dataConnection.Videos.Skip(skip).Take(offset).ToList();
            }
        }

        public async Task<Video> AddAsync(Video entity)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                entity.Id = await dataConnection.InsertWithInt32IdentityAsync(entity);

                return entity;
            }
        }

        public async Task<Video> UpdateAsync(Video entity)
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
                await dataConnection.Videos.Where(x => x.Id == id).DeleteAsync();
            }
        }

        public async Task RemoveAsync(int[] ids)
        {
            using (var dataConnection = new DatabaseConnection())
            {
                await dataConnection.Categories.Where(x => ids.Contains(x.Id)).DeleteAsync();
            }
        }
    }
}
