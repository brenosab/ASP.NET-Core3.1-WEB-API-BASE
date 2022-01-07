using APIorm.Models;
using APIorm.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIorm.Services.Interfaces;
using System;
using APIorm.Models.Interface;
using APIorm.Exceptions;

namespace APIorm.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository repository;
        public ProdutoService(IProdutoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ResponseCluster<IEnumerable<Produto>>> GetAll(int pageIndex, int pageSize)
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
        public Produto Get(int id, string descricao)
        {
            try
            {
                return repository.Get(id, descricao);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Task<ResponseCluster<IEnumerable<Produto>>> GetProdutoList(IEnumerable<int> idList)
        {
            try
            {
                return repository.GetProdutoList(idList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Produto> Put(long id, Produto produto)
        {
            var exist = await repository.ExistsAsync(id);
            if(!exist)
                throw new ApiException(ApiException.ApiExceptionReason.PRODUTO_NAO_ENCONTRADO, "Produto não encontrado");
            
            produto.IdProduto = id;
            return await repository.UpdateAsync(produto);
        }
        public Task<Produto> Post(Produto produto)
        {
            produto.DataHoraCadastro = DateTime.Now;
            return repository.AddAsync(produto);
        }
        public Task<string> PostProdutoList(IEnumerable<Produto> produtoList)
        {
            try
            {
                return repository.PostProdutoList(produtoList);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Task<Produto> Delete(long id)
        {
            try
            {
                return repository.DeleteProduto(id);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}