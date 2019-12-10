using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IShopify.Core.Data
{
    public interface IDeleteDataRepository<TEntity, T> where TEntity : IEntity<T> where T : struct
    {
        Task DeleteAsync(T id, bool throwIfNotFound = true);

        Task DeleteAllAsync(IEnumerable<T> ids);

        Task DeleteAllAsync(DateTime lastDeletedDate);

        Task DeleteAllAsync(Expression<Func<TEntity, bool>> expression);
    }
}