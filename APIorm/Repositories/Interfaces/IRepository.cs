using APIorm.Models.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace APIorm.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> ExistsAsync(long id);
        Task<TEntity> DeleteAsync(long id);
        Task<ResponseCluster<IQueryable<TEntity>>> GetAll(int pageIndex, int pageSize);
    }
}