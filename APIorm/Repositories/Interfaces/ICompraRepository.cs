using APIorm.Models;
using APIorm.Models.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIorm.Repositories.Interfaces
{
    public interface ICompraRepository : IRepository<Compra>
    {
        Task<ResponseCluster<IEnumerable<Compra>>> GetAll(int pageIndex, int pageSize);
        Task<Compra> Get(long id);
        Task<ResponseCluster<IEnumerable<Compra>>> GetCompraList(IEnumerable<long> idList);
        Task<Compra> Put(long id, Compra compra);
        Task<string> PostCompraList(IEnumerable<Compra> compraList);
        Task<Compra> DeleteCompra(long id);
    }
}