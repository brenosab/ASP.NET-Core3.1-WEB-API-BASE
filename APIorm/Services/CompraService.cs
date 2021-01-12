using APIorm.Models;
using APIorm.Models.Interface;
using APIorm.Repositories.Interfaces;
using APIorm.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIorm.Services
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _repository;
        public CompraService(ICompraRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseCluster<IEnumerable<Compra>>> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                return await _repository.GetAll(pageIndex, pageSize);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Task<Compra> Get(long id)
        {
            try
            {
                return _repository.Get(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Task<ResponseCluster<IEnumerable<Compra>>> GetCompraList(IEnumerable<long> idList)
        {
            try
            {
                return _repository.GetCompraList(idList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Task<Compra> PutCompra(long id, Compra compra)
        {
            try
            {
                return _repository.PutCompra(id, compra);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Task<Compra> PostCompra(Compra compra)
        {
            try
            {
                return _repository.PostCompra(compra);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Task<string> PostCompraList(IEnumerable<Compra> compraList)
        {
            try
            {
                return _repository.PostCompraList(compraList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Task<Compra> DeleteCompra(long id)
        {
            try
            {
                return _repository.DeleteCompra(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}