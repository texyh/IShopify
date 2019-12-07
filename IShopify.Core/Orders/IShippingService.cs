using IShopify.Core.Orders.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Orders
{
    public interface IShippingService
    {
        Task<IList<OrderAddressViewModel>> GetAllCustomerShippingAddressAsync();

        Task SaveShippingMethod(Guid orderId, ShippingMethod shippingMethod);

        Task<IList<OrderAddressViewModel>> GetAllCustomerBillingAddressAsync();

        Task SaveBillingAddressAsync(Guid orderId, SaveOrderAddressViewModel address);

        Task SaveOrderAddressAsync(Guid orderId, int addressId);

        Task SaveShippingAddressAsync(Guid orderId, SaveOrderAddressViewModel model);

        Task<OrderAddressViewModel> GetOrderBillingAddressAsync(Guid orderId);

        Task<OrderAddressViewModel> GetOrderShippingAddressAsync(Guid orderId);

    }
}
