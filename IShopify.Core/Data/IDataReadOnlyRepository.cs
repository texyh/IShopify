using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Data
{
    public interface IDataReadOnlyRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity> GetAsync(int id, bool allowNull = false);

        Task<IList<TEntity>> FindAllAsync(IEnumerable<int> Ids, bool includeDeleted = false);

        Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter);

        Task<IList<Guid>> FindAllIdsAsync(Expression<Func<TEntity, bool>> filter, bool includeDeleted = false);

        Task<long> CountAsync(Expression<Func<TEntity, bool>> filter);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);
    }
}
