using GestaoCompras.Domain.Entities;
using GestaoCompras.Domain.Entities.Interface;
using GestaoCompras.Domain.Repositories;
using GestaoCompras.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoCompras.Api.Services
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository repository;
        public CompraService(ICompraRepository repository)
        {
            this.repository = repository;
        }
        public async Task<ResponseCluster<IEnumerable<Compra>>> GetAll(int pageIndex, int pageSize)
        {
            return await repository.GetAll(pageIndex, pageSize);
        }
        public Task<Compra> Get(long id)
        {
            return repository.Get(id);
        }
        public Task<ResponseCluster<IEnumerable<Compra>>> GetCompraList(IEnumerable<long> idList)
        {
            return repository.GetList(idList);
        }
        public Task<Compra> Put(long id, Compra compra)
        {
            return repository.Put(id, compra);
        }
        public Task<Compra> Post(Compra compra)
        {
            return repository.AddAsync(compra);
        }
        public Task<string> PostCompraList(IEnumerable<Compra> compraList)
        {
            return repository.PostList(compraList);
        }
        public Task<Compra> DeleteCompra(long id)
        {
            return repository.Delete(id);
        }
    }
}