using GestaoCompras.Domain.Entities;
using GestaoCompras.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using GestaoCompras.Domain.Services;
using System;
using GestaoCompras.Domain.Entities.Interface;
using GestaoCompras.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using GestaoCompras.Domain.Dtos;
using GestaoCompras.Domain.Mapper;

namespace GestaoCompras.Api.Services
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
        public ProdutoDto Get(int id, string descricao)
        {
            var produto = repository.Get(id, descricao);

            var mapper = MapperConfig.InitializeAutomapper();
            ProdutoDto produtoDto = mapper.Map<ProdutoDto>(produto);

            return produtoDto;
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