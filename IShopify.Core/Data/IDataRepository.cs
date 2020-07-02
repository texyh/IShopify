using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Data
{
    public interface IDataRepository<TEntity, T> : 
        IDataReadOnlyRepository<TEntity, T>,
        IDeleteDataRepository<TEntity, T>
        where TEntity : IEntity<T> where T : struct
    {
        Task<T> AddAsync(TEntity entity);

        Task AddAllAsync(IEnumerable<TEntity> entities);
        
        Task UpdateAsync(TEntity entity);

        Task UpdateFieldsAsync(TEntity entity, params string[] fields);

        Task MarkEntityDeletedAsync(T id);

        Task MarkEntitiesDeletedAsync(Expression<Func<TEntity, bool>> filter);

        Task MarkEntitiesDeletedAsync(IEnumerable<T> ids);

        Task DeleteAsync(TEntity entity);
    }
}
