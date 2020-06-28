using APIorm.Models;
using APIorm.Models.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIorm.Repositories.Interfaces
{
    public interface ICompraRepository
    {
        Task<IEnumerable<Compra>> GetAll();
        Compra Get(long id);
        Task<ResponseCluster<IEnumerable<Compra>>> GetCompraList(IEnumerable<int> idList);
        Task<string> PutCompra(long id, Compra compra);
        Task<string> PostCompra(Compra compra);
        Task<string> PostCompraList(IEnumerable<Compra> compraList);

        Task<string> DeleteCompra(long id);
    }
}