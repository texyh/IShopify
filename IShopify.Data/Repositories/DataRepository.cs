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

        public Task DeleteAllAsync(IEnumerable<T> ids)
        {
            ArgumentGuard.NotNullOrEmpty(ids, nameof(ids));

            _dbContext.Set<TEntity>()
                .RemoveRange(ids.Select(x => GetDefaultObjectWithId(x)));

            return _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAllAsync(DateTime lastDeletedDate)
        {
            ArgumentGuard.NotDefault(lastDeletedDate, nameof(lastDeletedDate));

            var oldEntities = await _dbContext.Set<TEntity>()
                .Where(x => x.DeleteDateUtc != null && x.DeleteDateUtc <= lastDeletedDate)
                .ToListAsync();
            
            _dbContext.Set<TEntity>().RemoveRange(oldEntities);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAllAsync(Expression<Func<TEntity, bool>> expression)
        {
            var oldEntities = await _dbContext.Set<TEntity>()
                .Where(expression)
                .ToListAsync();

            _dbContext.Set<TEntity>().RemoveRange(oldEntities);

            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(TEntity entity)
        {
            ArgumentGuard.NotNull(entity, nameof(entity));

            _dbContext.Set<TEntity>().Remove(entity);

            return _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T id, bool throwIfNotFound = true)
        {
            ArgumentGuard.NotDefault(id, nameof(id));

            var entity = _dbContext.Set<TEntity>().Local.FirstOrDefault(x => x.Id.Equals(id));

            if(entity.IsNull()) 
            {
                _dbContext.Set<TEntity>().Remove(GetDefaultObjectWithId(id));

            } else {
                _dbContext.Set<TEntity>().Remove(entity);
            }
            
            var rows = await _dbContext.SaveChangesAsync();

            if(rows == 0 && throwIfNotFound) 
            {
                throw new ObjectNotFoundException($"{typeof(TEntity).Name} with id {id} was not found");
            }
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

        public Task MarkEntityDeletedAsync(T id)
        {
            ArgumentGuard.NotDefault(id, nameof(id));

            var entity = GetDefaultObjectWithId(id);
            MarkDeleted(entity);

            return UpdateFieldsAsync(entity, nameof(entity.DeleteDateUtc));
        }

        public async Task MarkEntitiesDeletedAsync(Expression<Func<TEntity, bool>> filter)
        {
            ArgumentGuard.NotNull(filter, nameof(filter));

            var entities = await _dbContext.Set<TEntity>()
                                     .Where(filter)
                                     .ToListAsync();
            
            if(!entities.IsNull()) 
            {
                MarkDeleted(entities);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task MarkEntitiesDeletedAsync(IEnumerable<T> ids)
        {
            ArgumentGuard.NotNull(ids, nameof(ids));

            var entities = await _dbContext.Set<TEntity>()
                    .Where(x => ids.Contains(x.Id))
                    .ToListAsync();

            if(entities.IsNotNull()) 
            {
                MarkDeleted(entities);
                await _dbContext.SaveChangesAsync();
            }
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

        private TEntity GetDefaultObjectWithId(T id) 
        {
            var type = typeof(TEntity);
            var instance = Activator.CreateInstance(type);
            var property = type.GetProperty("Id");
            property.SetValue(instance, id);

            return (TEntity)instance;
        }

        private void MarkDeleted(TEntity entity) 
        {
            (typeof(TEntity).GetProperty(nameof(entity.DeleteDateUtc))).SetValue(entity, DateTime.UtcNow);
        }

        private void MarkDeleted(IEnumerable<TEntity> entities) 
        {
            foreach (var entity in entities)
            {
                MarkDeleted(entity);
                _dbContext.Entry(entity).Property(x => x.DeleteDateUtc);
            }
        }
    }
}
