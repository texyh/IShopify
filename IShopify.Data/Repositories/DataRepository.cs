using AutoMapper;
using IShopify.Core.Data;
using IShopify.Core.Exceptions;
using IShopify.Core.Helpers;
using IShopify.Core.Products.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Data.Repositories
{
    internal abstract class DataRepository<TEntity, T> : IDataRepository<TEntity, T> where TEntity : class, IEntity<T> where T : struct
    {
        private readonly IShopifyDbContext _dbContext;

        private readonly IMapper _mapper;

        public DataRepository(
            IShopifyDbContext dbcontext,
            IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }
        public async Task AddAllAsync(IEnumerable<TEntity> entities)
        {
            ArgumentGuard.NotNullOrEmpty(entities, nameof(entities));

            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<T> AddAsync(TEntity entity)
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

        public Task DeleteAsync(TEntity entity)
        {
            ArgumentGuard.NotNull(entity, nameof(entity));

            _dbContext.Set<TEntity>().Remove(entity);

            return _dbContext.SaveChangesAsync();
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

        public async Task<IList<TEntity>> FindAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IList<TEntity>> FindAllInIdsAsync(IEnumerable<T> ids)
        {
            ArgumentGuard.NotNullOrEmpty(ids, nameof(ids));

            return await _dbContext.Set<TEntity>()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }

        public async Task<TEntity> GetAsync(T id, bool allowNull = false)
        {
            ArgumentGuard.NotDefault(id, nameof(id));

            var entity = _dbContext.Set<TEntity>().Local.FirstOrDefault(x => x.Id.Equals(id));

            if(entity.IsNull())
            {
                entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
            }
 
            if(entity == null && !allowNull)
            {
                throw new ObjectNotFoundException($"{typeof(TEntity).Name} with id {id} was not found");
            }

            return entity;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, bool allowNull = false)
        {
            ArgumentGuard.NotNull(expression, nameof(expression));

            var entity = _dbContext.Set<TEntity>().Local.FirstOrDefault(expression.Compile());

            if(entity.IsNull())
            {
                entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(expression);
            }

            if (entity.IsNull() &&  !allowNull)
            {
                throw new ObjectNotFoundException($"{typeof(TEntity).Name} not found");
            }

            return entity;
        }

        public Task UpdateAsync(TEntity entity)
        {
            ArgumentGuard.NotNull(entity, nameof(entity));

            _dbContext.Set<TEntity>().Update(entity);

            return _dbContext.SaveChangesAsync();
        }

        public Task UpdateFieldsAsync(TEntity entity, params string[] fields)
        {
            ArgumentGuard.NotNull(entity, nameof(entity));
            ArgumentGuard.NotNullOrEmpty(fields, nameof(fields));

            var dbEntity = _dbContext.Set<TEntity>().Local.FirstOrDefault(x => x.Id.Equals(entity.Id));

            if(dbEntity.IsNull())
            {
                dbEntity = entity;

                _dbContext.Set<TEntity>().Attach(dbEntity);
            } 
            else 
            {
                _mapper.Map(entity, dbEntity);
            }

            foreach (var field in fields)
            {
                _dbContext.Entry(dbEntity).Property(field).IsModified = true;
            }

            return _dbContext.SaveChangesAsync();
        }
    }
}
