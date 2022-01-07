using APIorm.Exceptions;
using APIorm.Models.Context;
using APIorm.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APIorm.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private readonly CompraContext context;

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