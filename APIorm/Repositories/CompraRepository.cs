using APIorm.Models;
using APIorm.Models.Context;
using APIorm.Models.Interface;
using APIorm.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIorm.Repositories
{
    public class CompraRepository : ICompraRepository
    {
        private readonly CompraContext _context;
        public CompraRepository(CompraContext context)
        {
            _context = context;
        }

        public Compra Get(long id)
        {
            try
            {
                //if (!_context.Database.CanConnect()) { throw new ProdutoException(ProdutoException.ProdutoExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }
                var compra = _context.Compras
                   .Select(b => b)
                   .Where(b => b.IdCompra == id)
                   .SingleOrDefault();

                compra.ItensCompra = _context.ItensCompras
                     .Select(i => i)
                     .Where(i => i.IdCompra == id)
                     .ToList();              
                
                foreach(ItensCompra itensCompra in compra.ItensCompra)
                {
                    itensCompra.Compra = null;
                    itensCompra.Produto = null;
                }
                //if (produto == null) { throw new ProdutoException(ProdutoException.ProdutoExceptionReason.PRODUTO_NAO_ENCONTRADO, "Produto não encontrado"); }

                return compra;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Compra>> GetAll()
        {
            try
            {
                //if (!_context.Database.CanConnect()) { throw new ProdutoException(ProdutoException.ProdutoExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }
                var compras = await _context.Compras.ToListAsync();
                foreach(Compra compra in compras)
                {
                    compra.ItensCompra = _context.ItensCompras
                        .Select(i => i)
                        .Where(i => i.IdCompra == compra.IdCompra)
                        .ToList();

                    foreach (ItensCompra itensCompra in compra.ItensCompra)
                    {
                        itensCompra.Compra = null;
                        itensCompra.Produto = null;
                    }
                }

                return compras;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ResponseCluster<IEnumerable<Compra>>> GetCompraList(IEnumerable<int> idList)
        {
            try
            {
                //if (!_context.Database.CanConnect()) { throw new ProdutoException(ProdutoException.ProdutoExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                List<Erro> erros = new List<Erro>() { };

                var compras = await _context.Compras
                    .Select(b => b)
                    .Where(b => idList.Contains(b.IdLoja)).ToListAsync();

                foreach (int id in idList)
                {
                    //if (produtos.Where(b => b.IdLoja == id).FirstOrDefault() == null) { erros.Add(new Erro { Id = id, Mensagem = "Produto não encontrado" }); }
                }
                if (erros.Any()) return new ResponseCluster<IEnumerable<Compra>>() { objValue = compras, erros = erros };

                return new ResponseCluster<IEnumerable<Compra>>() { objValue = compras };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> PutCompra(long id, Compra compra)
        {
            try
            {
                //if (!_context.Database.CanConnect()) { throw new ProdutoException(ProdutoException.ProdutoExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                _context.Entry(compra).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(id))
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

        public async Task<string> PostCompra(Compra compra)
        {
            try
            {
                //if (!_context.Database.CanConnect()) { throw new ProdutoException(ProdutoException.ProdutoExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                _context.Compras.Add(compra);
                await _context.SaveChangesAsync();
                return string.Empty;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<string> PostCompraList(IEnumerable<Compra> compraList)
        {
            try
            {
                //if (!_context.Database.CanConnect()) { throw new ProdutoException(ProdutoException.ProdutoExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                _context.Compras.AddRange(compraList);
                await _context.SaveChangesAsync();

                return string.Empty;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> DeleteCompra(long id)
        {
            try
            {
                //if (!_context.Database.CanConnect()) { throw new ProdutoException(ProdutoException.ProdutoExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                var produto = await _context.Compras.FindAsync(id);
                if (produto == null)
                {
                    //throw new Exception("Produto não encontrado.");
                }
                _context.Compras.Remove(produto);
                await _context.SaveChangesAsync();

                return string.Empty;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private bool CompraExists(long id)
        {
            return _context.Compras.Any(e => e.IdCompra == id);
        }

    }
}