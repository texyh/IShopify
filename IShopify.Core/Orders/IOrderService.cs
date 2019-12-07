using IShopify.Core.Orders.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Orders
{
    public interface IOrderService
    {
        Task<Guid> CreateAsync(IList<SaveOrderItemViewModel> orderItems);
        
        Task ConfirmOrder(Guid Id);

        Task<IList<OrderItem>> GetOrderPurchasedItems(Guid orderId);
    }
}
