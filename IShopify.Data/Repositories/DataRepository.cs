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
            ArgumentGuard.NotNullOrEmpty(entities, nameof(entities));

            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<int> AddAsync(TEntity entity)
        {
            ArgumentGuard.NotNull(entity, nameof(entity));

            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            ArgumentGuard.NotNull(filter, nameof(filter));

            return  await _dbContext.Set<TEntity>().CountAsync(filter);
        }

        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
        {
            ArgumentGuard.NotNull(filter, nameof(filter));

            return _dbContext.Set<TEntity>().AnyAsync(filter);
        }

        public Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            ArgumentGuard.NotNull(filter, nameof(filter));

            throw new NotImplementedException();
        }


        public Task<IList<TEntity>> FindAllAsync(IEnumerable<int> Ids)
        {
            ArgumentGuard.NotNullOrEmpty(Ids, nameof(Ids));

            throw new NotImplementedException();
        }

        public Task<IList<int>> FindAllIdsAsync(Expression<Func<TEntity, bool>> filter)
        {
            ArgumentGuard.NotNull(filter, nameof(filter));

            throw new NotImplementedException();
        }

        public async Task<TEntity> GetAsync(int id, bool allowNull = false)
        {
            ArgumentGuard.NotDefault(id, nameof(id));

            var entity = await _dbContext.Set<TEntity>().FindAsync(id);

            if(entity == null && !allowNull)
            {
                throw new ObjectNotFoundException($"No record with id {id} was found");
            }

            return entity;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, bool allowNull = false)
        {
            ArgumentGuard.NotNull(expression, nameof(expression));

            var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(expression);
            
            if(entity.IsNull() &&  !allowNull)
            {
                throw new ObjectNotFoundException($"{nameof(TEntity)} not found");
            }

            return entity;
        }

        public Task UpdateAsync(TEntity entity)
        {
            ArgumentGuard.NotNull(entity, nameof(entity));

            _dbContext.Set<TEntity>().Update(entity);

            return _dbContext.SaveChangesAsync();
        }

        public Task UpdateSingleField(TEntity entity, Expression<Func<TEntity, object>> expression)
        {
            ArgumentGuard.NotNull(entity, nameof(entity));
            ArgumentGuard.NotDefault(entity.Id, nameof(entity.Id));
            ArgumentGuard.NotNull(expression, nameof(expression));

            var dbLocalEntity = _dbContext.Set<TEntity>().Local.FirstOrDefault(x => x.Id == entity.Id);

            if(dbLocalEntity.IsNull())
            {
                _dbContext.Set<TEntity>().Attach(dbLocalEntity);
            }

            _dbContext.Entry(dbLocalEntity).Property(expression).IsModified = true;
            return _dbContext.SaveChangesAsync();
        }
    }
}
