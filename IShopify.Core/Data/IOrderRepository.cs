using IShopify.Core.Orders;
using IShopify.Core.Orders.Models;
using IShopify.Core.Orders.Models.Entities;
using IShopify.Core.Orders.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Data
{
    public interface IOrderRepository : IDataRepository<OrderEntity, Guid>
    {
        Task CreateOrder(OrderEntity order, IList<OrderItemEntity> orderItems);

        Task<IList<AddressEntity>> GetAllCustomerAddressAsync(int custonerId, bool IsBilling = false);

        Task SaveOrderAddressAsync(AddressEntity model);

        Task<OrderEntity> GetAsync(Guid id, bool allowNull = false, bool isSummary = false);

        Task<AddressEntity> GetOrderAddress(Guid id, bool isBilling = false);

        Task<IList<OrderItemEntity>> GetOrderItemsAsync(Guid id, bool includeProduct = false);
    }
}
