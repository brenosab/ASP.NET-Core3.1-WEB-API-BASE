using APIorm.Exceptions;
using APIorm.Models;
using APIorm.Models.Context;
using APIorm.Models.Interface;
using APIorm.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace APIorm.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        private readonly CompraContext _context;
        private const int DefaultPageIndex = 1;
        private const int DefaultPageSize = 10;

        public ProdutoRepository(CompraContext context) : base(context)
        {
            _context = context;
        }

        public Produto Get(int id, string descricao)
        {
            if (!_context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");

            var produto = _context.Produto
                .Select(b => b)
                .Where(b => b.IdProduto == id || b.Descricao.Contains(descricao))
                .SingleOrDefault();

            if(produto == null) throw new ApiException(ApiException.ApiExceptionReason.PRODUTO_NAO_ENCONTRADO, "Produto não encontrado");
            return produto;
        }

        public async Task<ResponseCluster<IEnumerable<Produto>>> GetAll(int pageIndex, int pageSize)
        {
            if (!_context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");

            pageSize = pageSize == 0 ? DefaultPageSize : pageSize;
            pageIndex = pageIndex == 0 ? DefaultPageIndex : pageIndex;
            var produtos = await _context.Produto.ToPagedListAsync(pageIndex, pageSize);
            var count = produtos.TotalItemCount;

            return new ResponseCluster<IEnumerable<Produto>>() { 
                objValue = produtos.ToList(), 
                totalItemCount = count, 
                metaData = produtos.GetMetaData() 
            };
        }

        public async Task<ResponseCluster<IEnumerable<Produto>>> GetList(IEnumerable<int> idList)
        {
            if (!_context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");
            List<Erro> erros = new List<Erro>() { };                
            var produtos = await _context.Produto
                .Select(b => b)
                .Where(b => idList.Contains(b.Codigo)).ToListAsync();
                
            foreach(int id in idList)
            {                    
                if(produtos.Where(b => b.Codigo == id).FirstOrDefault() == null) { erros.Add(new Erro { Id = id, Mensagem = "Produto não encontrado" }); }
            }
            if (erros.Any()) return new ResponseCluster<IEnumerable<Produto>>() { objValue = produtos, erros = erros };

            return new ResponseCluster<IEnumerable<Produto>>() { objValue = produtos };
        }

        public async Task<string> PostList(IEnumerable<Produto> produtoList)
        {
            if (!_context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");

            _context.Produto.AddRange(produtoList);
            await _context.SaveChangesAsync();
            return string.Empty;
        }

        public async Task<Produto> Delete(long id)
        {
            if (!_context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");

            var produto = await _context.Produto.FindAsync(id);
            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();
            return produto;
        }
    }
}