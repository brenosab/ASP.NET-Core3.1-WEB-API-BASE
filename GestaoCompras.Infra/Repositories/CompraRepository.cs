using GestaoCompras.Domain.Exceptions;
using GestaoCompras.Domain.Entities;
using GestaoCompras.Infra.Context;
using GestaoCompras.Domain.Entities.Interface;
using GestaoCompras.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GestaoCompras.Infra.Repositories
{
    public class CompraRepository : Repository<Compra>, ICompraRepository
    {
        private readonly CompraContext _context;
        private const int DefaultPageIndex = 1;
        private const int DefaultPageSize = 10;

        public CompraRepository(CompraContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Compra> Get(long id)
        {
            if (!_context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");
            var compra = await _context.Compra
                .Select(b => b)
                .Where(b => b.IdCompra == id)
                .SingleOrDefaultAsync();

            if (compra == null) throw new ApiException(ApiException.ApiExceptionReason.COMPRA_NAO_ENCONTRADA, "Compra não encontrada");

            compra.ItensCompra = await _context.ItensCompra
                    .Select(i => i)
                    .Where(i => i.IdCompra == id)
                    .ToListAsync();              
                
            foreach(ItemCompra itensCompra in compra.ItensCompra)
            {
                itensCompra.Compra = null;
                itensCompra.Produto = null;
            }
            return compra;
        }

        public new async Task<ResponseCluster<IEnumerable<Compra>>> GetAll(int pageIndex, int pageSize)
        {
            if (!_context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");

            pageSize = pageSize == 0 ? DefaultPageSize : pageSize;
            pageIndex = pageIndex == 0 ? DefaultPageIndex : pageIndex;

            var compras = await _context.Compra.ToPagedListAsync(pageIndex, pageSize);
            var count = compras.TotalItemCount;

            foreach(Compra compra in compras)
            {
                compra.ItensCompra = _context.ItensCompra
                    .Select(i => i)
                    .Where(i => i.IdCompra == compra.IdCompra)
                    .ToList();
                    
                foreach (ItemCompra itensCompra in compra.ItensCompra)
                {
                    itensCompra.Compra = null;
                    itensCompra.Produto = null;
                }
            }

            return new ResponseCluster<IEnumerable<Compra>>() { objValue = compras, totalItemCount = count };
        }

        public async Task<ResponseCluster<IEnumerable<Compra>>> GetList(IEnumerable<long> idList)
        {            
            if (!_context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");
                
            List<Erro> erros = new List<Erro>() { };
                
            var compras = await _context.Compra
                .Select(b => b)
                .Where(b => idList.Contains(b.IdCompra)).ToListAsync();

            foreach (long id in idList)
            {
                if (compras.Where(b => b.IdCompra == id).FirstOrDefault() == null) { erros.Add(new Erro { Id = int.Parse(id.ToString()), Mensagem = "Compra não encontrada" }); }
            }
            if (erros.Any()) return new ResponseCluster<IEnumerable<Compra>>() { objValue = compras, erros = erros };

            foreach (Compra compra in compras)
            {
                compra.ItensCompra = _context.ItensCompra
                .Select(i => i)
                .Where(i => i.IdCompra == compra.IdCompra)
                .ToList();

                foreach (ItemCompra itensCompra in compra.ItensCompra)
                {
                    itensCompra.Compra = null;
                    itensCompra.Produto = null;
                }
            }
                
            return new ResponseCluster<IEnumerable<Compra>>() { objValue = compras };
        }

        public async Task<Compra> Put(long id, Compra compra)
        {
            if (!_context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");

            var itens = await _context.ItensCompra
            .Select(i => i)
            .Where(i => i.IdCompra == id)
            .ToListAsync();

            _context.ItensCompra.RemoveRange(itens);
            _context.ItensCompra.AddRange(compra.ItensCompra);
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
            return compra;
        }

        public async Task<string> PostList(IEnumerable<Compra> compraList)
        {
            if (!_context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");

            foreach (Compra compra in compraList)
            {
                _context.Compra.Add(compra);
            }
            await _context.SaveChangesAsync();

            return string.Empty;
        }

        public async Task<Compra> Delete(long id)
        {
            if (!_context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");
            var compra = await _context.Compra.FindAsync(id);
            var itens = await _context.ItensCompra
                .Select(i => i)
                .Where(i => i.IdCompra == id)
                .ToListAsync();

            if (compra == null)
            {
                throw new ApiException(ApiException.ApiExceptionReason.COMPRA_NAO_ENCONTRADA, "Compra não encontrada");
            }
            _context.ItensCompra.RemoveRange(itens);
            _context.Compra.Remove(compra);
            await _context.SaveChangesAsync();

            return compra;
        }

        private bool CompraExists(long id)
        {
            return _context.Compra.Any(e => e.IdCompra == id);
        }
    }
}