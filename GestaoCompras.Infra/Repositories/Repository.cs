using GestaoCompras.Domain.Exceptions;
using GestaoCompras.Infra.Context;
using GestaoCompras.Domain.Entities.Interface;
using GestaoCompras.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GestaoCompras.Infra.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private readonly CompraContext context;
        private const int DefaultPageIndex = 1;
        private const int DefaultPageSize = 10;
        public Repository(CompraContext context)
        {
            this.context = context;
            if (!context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");
        }
        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return context.Set<TEntity>();
            }
            catch (Exception)
            {
                throw new Exception("Couldn't retrieve entities");
            }
        }
        public async Task<ResponseCluster<IQueryable<TEntity>>> GetAll(int pageIndex, int pageSize)
        {
            if (!context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");

            pageSize = pageSize == 0 ? DefaultPageSize : pageSize;
            pageIndex = pageIndex == 0 ? DefaultPageIndex : pageIndex;
            var entities = await context.Set<TEntity>().ToPagedListAsync(pageIndex, pageSize);
            var count = entities.TotalItemCount;

            return new ResponseCluster<IQueryable<TEntity>>()
            {
                objValue = entities.AsQueryable(),
                totalItemCount = count,
                metaData = entities.GetMetaData()
            };
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }
            if (!context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");

            try
            {
                await context.AddAsync(entity);
                await context.SaveChangesAsync();

                return entity;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("chave duplicada"))
                    throw new ApiException(ApiException.ApiExceptionReason.DB_CHAVE_DUPLICADA, ex.InnerException.Message.Replace("\r\n", ""));
                else
                    throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, ex.InnerException.Message);
            }
            catch (Exception)
            {
                throw new Exception($"{nameof(entity)} could not be saved");
            }
        }

        public async Task<TEntity> DeleteAsync(long id)
        {
            if (!context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");

            var entity = await context.Set<TEntity>().FindAsync(id);
            context.Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }
            if (!context.Database.CanConnect()) throw new ApiException(ApiException.ApiExceptionReason.DB_CONNECTION_NOT_COMPLETED, "Não foi possível abrir conexão com banco de dados");
            try
            {
                context.Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {
                throw new Exception($"{nameof(entity)} could not be updated");
            }
        }
        public async Task<bool> ExistsAsync(long id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            return entity != null;
        }
    }
}