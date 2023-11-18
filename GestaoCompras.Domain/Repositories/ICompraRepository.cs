using GestaoCompras.Domain.Entities;
using GestaoCompras.Domain.Entities.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoCompras.Domain.Repositories
{
    public interface ICompraRepository : IRepository<Compra>
    {
        new Task<ResponseCluster<IEnumerable<Compra>>> GetAll(int pageIndex, int pageSize);
        Task<Compra> Get(long id);
        Task<ResponseCluster<IEnumerable<Compra>>> GetList(IEnumerable<long> idList);
        Task<Compra> Put(long id, Compra compra);
        Task<string> PostList(IEnumerable<Compra> compraList);
        Task<Compra> Delete(long id);
    }
}