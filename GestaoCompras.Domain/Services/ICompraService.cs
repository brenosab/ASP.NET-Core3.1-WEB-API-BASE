using GestaoCompras.Domain.Entities.Interface;
using GestaoCompras.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoCompras.Domain.Services
{
    public interface ICompraService
    {
        Task<ResponseCluster<IEnumerable<Compra>>> GetAll(int pageIndex, int pageSize);
        Task<Compra> Get(long id);
        Task<ResponseCluster<IEnumerable<Compra>>> GetCompraList(IEnumerable<long> idList);
        Task<Compra> Put(long id, Compra compra);
        Task<Compra> Post(Compra compra);
        Task<string> PostCompraList(IEnumerable<Compra> compraList);

        Task<Compra> DeleteCompra(long id);
    }
}