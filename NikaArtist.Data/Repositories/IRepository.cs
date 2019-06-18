using System.Collections.Generic;
using System.Threading.Tasks;

namespace NikaArtist.Data.Repositories
{
    public interface IRepository<TEntity>
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll(int take, int skip);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task RemoveAsync(int id);
        Task RemoveAsync(int[] ids);
    }
}
