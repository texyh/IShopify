using IShopify.Core.Orders.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Orders
{
    public interface IOrderService
    {
        Task<int> CreateAsync(IList<SaveOrderItemViewModel> orderItems);

        Task<OrderAddressViewModel> GetBillingAddressAsync();

        Task SaveOrderAddressAsync(int id, SaveOrderAddressViewModel model);

        Task SaveOrderAddressAsync(int id, int shippingAddressId);

        Task<IList<OrderAddressViewModel>> GetAllShippingAddressAsync();

        Task SaveShippingMethodAddress(ShippingMethod shippingMethod);

        Task ConfirmOrder(int Id);

        Task<OrderSummary> GetSummary(int id);
    }
}
