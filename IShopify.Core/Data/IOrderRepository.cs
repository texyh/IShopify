using IShopify.Core.Orders.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Data
{
    public class IOrderRepository : IDataRepository<OrderEntity, Guid>
    {
        public Task AddAllAsync(IEnumerable<OrderEntity> documents)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> AddAsync(OrderEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(Expression<Func<OrderEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(OrderEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Expression<Func<OrderEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<IList<OrderEntity>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IList<OrderEntity>> FindAllAsync(Expression<Func<OrderEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<IList<OrderEntity>> FindAllInIdsAsync(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<OrderEntity> GetAsync(Guid id, bool allowNull = false)
        {
            throw new NotImplementedException();
        }

        public Task<OrderEntity> GetAsync(Expression<Func<OrderEntity, bool>> expression, bool allowNull = false)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(OrderEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFieldsAsync(OrderEntity entity, params string[] fields)
        {
            throw new NotImplementedException();
        }
    }
}
