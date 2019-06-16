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

        Task MarkentityDeletedAsync(int entityId);

        Task<long> MarkDocumentsDeletedAsync(Expression<Func<TEntity, bool>> filter);

        Task<long> MarkDocumentsDeletedAsync(IEnumerable<int> entityIds);
    }
}
