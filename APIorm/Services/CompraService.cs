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

        public async Task<IEnumerable<Compra>> GetAll()
        {
            try
            {
                return await _repository.GetAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Compra Get(long id)
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
        public Task<ResponseCluster<IEnumerable<Compra>>> GetCompraList(IEnumerable<int> idList)
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


        public Task<string> PutCompra(long id, Compra compra)
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
        public Task<string> PostCompra(Compra compra)
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
        public Task<string> DeleteCompra(long id)
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