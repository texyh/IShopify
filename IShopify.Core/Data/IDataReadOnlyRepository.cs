using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Data
{
    public interface IDataReadOnlyRepository<TEntity, T> where TEntity : IEntity<T> where T : struct
    {
        Task<TEntity> GetAsync(T id, bool allowNull = false);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, bool allowNull = false);

        Task<IList<TEntity>> FindAllAsync();

        Task<IList<TEntity>> FindAllInIdsAsync(IEnumerable<T> ids);

        Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter);

        Task<long> CountAsync(Expression<Func<TEntity, bool>> filter);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);
    }
}
