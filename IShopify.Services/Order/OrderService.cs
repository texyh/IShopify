using IShopify.Core.Orders;
using IShopify.Core.Orders.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.DomainServices.Order
{
    public class OrderService : IOrderService
    {
        public Task ConfirmOrder(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateAsync(IList<SaveOrderItemViewModel> orderItems)
        {
            throw new NotImplementedException();
        }

        public Task<IList<OrderAddressViewModel>> GetAllShippingAddressAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderAddressViewModel> GetBillingAddressAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderSummary> GetSummary(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveOrderAddressAsync(int id, SaveOrderAddressViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task SaveOrderAddressAsync(int id, int shippingAddressId)
        {
            throw new NotImplementedException();
        }

        public Task SaveShippingMethodAddress(ShippingMethod shippingMethod)
        {
            throw new NotImplementedException();
        }
    }
}
