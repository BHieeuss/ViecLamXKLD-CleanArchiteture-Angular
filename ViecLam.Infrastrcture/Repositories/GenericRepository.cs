using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using ViecLam.Application.Contracts.Persistances;
using ViecLam.Infrastructure.Context;

namespace ViecLam.Infrastructure.Repositories
{

    public class GenericRepository<T> : IGenericReponsitory<T> where T : class
    {
        private readonly AppDbContext dbContext;
        private readonly ILogger<GenericRepository<T>> logger;

        public GenericRepository(AppDbContext dbContext, ILogger<GenericRepository<T>> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await dbContext.AddAsync(entity);
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding entity to the database!");
                throw;
            }
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            try
            {
                var entity = await dbContext.Set<T>().FindAsync(id);
                if (entity != null)
                {
                    dbContext.Entry(entity).State = EntityState.Detached;
                }
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving entity from the database!");
                throw;
            }
        }

        public async Task DeleteAsync(object id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    dbContext.Remove(entity);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting entity from the database!");
                throw;
            }
        }
        public async Task UpdateAsync(T entity)
        {
            try
            {
                dbContext.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating entity in the database!");
                throw;
            }
        }

        public Task SaveChangeAsync() => dbContext.SaveChangesAsync();

        public IDbContextTransaction BeginTransaction()
        {
            return dbContext.Database.BeginTransaction();
        }
    }
}
