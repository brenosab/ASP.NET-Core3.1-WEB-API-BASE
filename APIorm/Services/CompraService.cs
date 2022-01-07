using APIorm.Exceptions;
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
        private readonly ICompraRepository repository;
        public CompraService(ICompraRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ResponseCluster<IEnumerable<Compra>>> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                return await repository.GetAll(pageIndex, pageSize);
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
                return repository.Get(id);
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
                return repository.GetCompraList(idList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Compra> Put(long id, Compra compra)
        {
            var exist = await repository.ExistsAsync(id);
            if (!exist)
                throw new ApiException(ApiException.ApiExceptionReason.PRODUTO_NAO_ENCONTRADO, $"Compra não encontrada");

            compra.IdCompra = id;
            return await repository.UpdateAsync(compra);
        }
        public Task<Compra> Post(Compra compra)
        {
            return repository.AddAsync(compra);
        }
        public Task<Compra> PostCompra(Compra compra)
        {
            try
            {
                return repository.PostCompra(compra);
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
                return repository.PostCompraList(compraList);
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
                return repository.DeleteCompra(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}