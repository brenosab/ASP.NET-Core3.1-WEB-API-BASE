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
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }
                var compra = _context.Compras
                   .Select(b => b)
                   .Where(b => b.IdCompra == id)
                   .SingleOrDefault();

                if (compra == null) { throw new ApiException(ApiException.ApiExceptionReason.COMPRA_NAO_ENCONTRADA, "Compra não encontrada"); }

                compra.ItensCompra = _context.ItensCompras
                     .Select(i => i)
                     .Where(i => i.IdCompra == id)
                     .ToList();              
                
                foreach(ItensCompra itensCompra in compra.ItensCompra)
                {
                    itensCompra.Compra = null;
                    itensCompra.Produto = null;
                }
                
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
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }
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

        public async Task<ResponseCluster<IEnumerable<Compra>>> GetCompraList(IEnumerable<long> idList)
        {            
            try
            {
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }
                
                List<Erro> erros = new List<Erro>() { };
                
                var compras = await _context.Compras
                    .Select(b => b)
                    .Where(b => idList.Contains(b.IdCompra)).ToListAsync();

                foreach (long id in idList)
                {
                    if (compras.Where(b => b.IdCompra == id).FirstOrDefault() == null) { erros.Add(new Erro { Id = int.Parse(id.ToString()), Mensagem = "Compra não encontrada" }); }
                }
                if (erros.Any()) return new ResponseCluster<IEnumerable<Compra>>() { objValue = compras, erros = erros };

                foreach (Compra compra in compras)
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
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                // ITENS
                var itens = await _context.ItensCompras
                .Select(i => i)
                .Where(i => i.IdCompra == id)
                .ToListAsync();

                _context.ItensCompras.RemoveRange(itens);
                _context.ItensCompras.AddRange(compra.ItensCompra);
                _context.Entry(compra).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(id))
                    {
                        throw new ApiException(ApiException.ApiExceptionReason.COMPRA_NAO_ENCONTRADA, "Compra não encontrada");
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
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }
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
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }

                foreach (Compra compra in compraList)
                {
                    _context.Compras.Add(compra);
                }
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
                if (!_context.Database.CanConnect()) { throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados"); }
                var compra = await _context.Compras.FindAsync(id);
                var itens = await _context.ItensCompras
                    .Select(i => i)
                    .Where(i => i.IdCompra == id)
                    .ToListAsync();

                if (compra == null)
                {
                    throw new ApiException(ApiException.ApiExceptionReason.COMPRA_NAO_ENCONTRADA, "Compra não encontrada");
                }
                _context.ItensCompras.RemoveRange(itens);
                _context.Compras.Remove(compra);
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