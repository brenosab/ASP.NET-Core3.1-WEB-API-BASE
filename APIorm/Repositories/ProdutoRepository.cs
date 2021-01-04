using APIorm.Exceptions;
using APIorm.Models;
using APIorm.Models.Context;
using APIorm.Models.Interface;
using APIorm.Repositories.Interfaces;
using APIorm.ViewModels;
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

        public Produto Get(int id)
        {
            try
            {
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                var produto = _context.Produtos
                    .Select(b => b)
                    .Where(b => b.IdProduto == id)
                    .SingleOrDefault();
                if(produto == null) { throw new ApiException(ApiException.ApiExceptionReason.PRODUTO_NAO_ENCONTRADO, "Produto não encontrado"); }

                return produto;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<ResponseCluster<IEnumerable<Produto>>> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                pageSize = pageSize == 0 ? DefaultPageSize : pageSize;
                pageIndex = pageIndex == 0 ? DefaultPageIndex : pageIndex;

                var produtos = await _context.Produtos.ToPagedListAsync(pageIndex, pageSize);
                var count = produtos.TotalItemCount;

                return new ResponseCluster<IEnumerable<Produto>>() { 
                    objValue = produtos.ToList(), 
                    totalItemCount = count, 
                    metaData = produtos.GetMetaData() 
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ResponseCluster<IEnumerable<Produto>>> GetProdutoList(IEnumerable<int> idList)
        {
            try
            {
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                List<Erro> erros = new List<Erro>() { };
                
                var produtos = await _context.Produtos
                    .Select(b => b)
                    .Where(b => idList.Contains(b.Codigo)).ToListAsync();
                
                foreach(int id in idList)
                {                    
                    if(produtos.Where(b => b.Codigo == id).FirstOrDefault() == null) { erros.Add(new Erro { Id = id, Mensagem = "Produto não encontrado" }); }
                }
                if (erros.Any()) return new ResponseCluster<IEnumerable<Produto>>() { objValue = produtos, erros = erros };

                return new ResponseCluster<IEnumerable<Produto>>() { objValue = produtos };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> PutProduto(long id, Produto produto)
        {
            try
            {
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                _context.Entry(produto).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(id))
                    {
                        throw new Exception("Produto não encontrado.");
                    }
                    else
                    {
                        throw;
                    }
                }
                return string.Empty;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Produto> PostProduto(Produto produto)
        {
            try
            {
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();
                return produto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<string> PostProdutoList(IEnumerable<Produto> produtoList)
        {
            try
            {
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                _context.Produtos.AddRange(produtoList);
                await _context.SaveChangesAsync();              
                
                return string.Empty;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> DeleteProduto(long id)
        {
            try
            {
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                var produto = await _context.Produtos.FindAsync(id);
                if (produto == null)
                {
                    throw new Exception("Produto não encontrado.");
                }
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();

                return string.Empty;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private bool ProdutoExists(long id)
        {
            return _context.Produtos.Any(e => e.IdProduto == id);
        }
    }
}