using IShopify.Core.Data;
using IShopify.Core.Exceptions;
using IShopify.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> AddAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public Task<long> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            return _dbContext.Set<TEntity>().AnyAsync(filter);
        }

        public Task<IList<TEntity>> FindAllAsync(IEnumerable<Guid> Ids)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }


        public Task<IList<TEntity>> FindAllAsync(IEnumerable<int> Ids)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Guid>> FindAllIdsAsync(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetAsync(int id, bool allowNull = false)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            if(entity == null && !allowNull)
            {
                throw new ObjectNotFoundException($"{nameof(TEntity)} with id {id} is not found");
            }

            return entity;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, bool allowNull = false)
        {
            var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(expression);
            
            if(entity.IsNull() &&  !allowNull)
            {
                throw new ObjectNotFoundException($"{nameof(TEntity)} not found");
            }

            return entity;
        }

        public Task UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChangesAsync();
        }

        public Task UpdateSingleField(TEntity entity, Expression<Func<TEntity, object>> expression)
        {
            ArgumentGuard.NotDefault(entity.Id, nameof(entity.Id));

            var dbEntity = _dbContext.Set<TEntity>().Local.FirstOrDefault(x => x.Id == entity.Id);

            if(dbEntity.IsNull())
            {
                _dbContext.Set<TEntity>().Attach(dbEntity);
            }

            _dbContext.Entry(dbEntity).Property(expression).IsModified = true;
            return _dbContext.SaveChangesAsync();
        }
    }
}
