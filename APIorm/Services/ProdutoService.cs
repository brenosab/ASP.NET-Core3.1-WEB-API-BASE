using APIorm.Models;
using APIorm.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIorm.Services.Interfaces;
using System;
using APIorm.Models.Interface;
using APIorm.Exceptions;
using Microsoft.Extensions.Logging;
using FluentValidator;

namespace APIorm.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository repository;
        private readonly ILogger<ProdutoService> logger;

        public ProdutoService(IProdutoRepository repository, ILogger<ProdutoService> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<ResponseCluster<IEnumerable<Produto>>> GetAll(int pageIndex, int pageSize)
        {
            var produtos = await repository.GetAll(pageIndex, pageSize);
            return new ResponseCluster<IEnumerable<Produto>>
            {
                totalItemCount = produtos.totalItemCount,
                metaData = produtos.metaData,
                erros = produtos.erros,
                objValue = produtos.objValue,
            };
        }
        public Produto Get(int id, string descricao)
        {
            return repository.Get(id, descricao);
        }
        public Task<ResponseCluster<IEnumerable<Produto>>> GetProdutoList(IEnumerable<int> idList)
        {
            return repository.GetList(idList);
        }
        public async Task<Produto> Put(long id, Produto produto)
        {
            var exist = await repository.ExistsAsync(id);
            if (!exist)
            {
                logger.LogError("ProdutoService.Put","Produto não encontrado");
                throw new ApiException(ApiException.ApiExceptionReason.PRODUTO_NAO_ENCONTRADO, "Produto não encontrado");
            }

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
            return repository.PostList(produtoList);
        }
        public async Task<Produto> Delete(long id)
        {
            var exist = await repository.ExistsAsync(id);
            if (!exist)
            {
                logger.LogError("ProdutoService.Delete", "Produto não encontrado");
                throw new ApiException(ApiException.ApiExceptionReason.PRODUTO_NAO_ENCONTRADO, "Produto não encontrado");
            }
            return await repository.DeleteAsync(id);
        }
    }
}