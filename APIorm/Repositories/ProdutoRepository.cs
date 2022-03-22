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
    }
}