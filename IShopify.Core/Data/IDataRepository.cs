using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Data
{
    public interface IDataRepository<TEntity> : IDataReadOnlyRepository<TEntity> where TEntity : IEntity
    {
        Task<int> AddAsync(TEntity entity);

        Task AddAllAsync(IEnumerable<TEntity> documents);
        
        Task UpdateAsync(TEntity entity);

        Task UpdateSingleField(TEntity entity, Expression<Func<TEntity, object>> expression);
    }
}
