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
            return await repository.GetAll(pageIndex, pageSize);
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
            return repository.PostList(produtoList);
        }
        public Task<Produto> Delete(long id)
        {
            return repository.Delete(id);
        }
    }
}