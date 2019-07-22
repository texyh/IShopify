using IShopify.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Data.Repositories
{
    internal abstract class DataRepository<TEntity> : IDataRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly IShopifyDbContext _dbContext;

        public DataRepository(IShopifyDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        public async Task AddAllAsync(IEnumerable<TEntity> entities)
        {
             await _dbContext.Set<TEntity>().AddRangeAsync(entities);
             await _dbContext.SaveChangesAsync();
        }

        public Task<int> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TEntity>> FindAllAsync(IEnumerable<Guid> Ids, bool includeDeleted = false)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TEntity>> FindAllAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TEntity>> FindAllAsync(IEnumerable<int> Ids, bool includeDeleted = false)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TEntity>> FindAllDeletedAsync(DateTime? maxDeletedDate = null)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Guid>> FindAllIdsAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter, bool includeDeleted = false)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetAsync(int id, bool allowNull = false)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            if(entity == null && !allowNull)
            {
                throw new Exception("No entity found"); //TODO change when adding exceptions;
            }

            return entity;
        }

        public Task<long> MarkDocumentsDeletedAsync(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<long> MarkDocumentsDeletedAsync(IEnumerable<int> entityIds)
        {
            throw new NotImplementedException();
        }

        public Task MarkentityDeletedAsync(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
