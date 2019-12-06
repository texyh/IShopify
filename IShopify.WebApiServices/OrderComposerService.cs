using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IShopify.Core.Orders;

namespace IShopify.WebApiServices
{
    public class OrderComposerService : IOrderComposerService
    {
        private readonly IOrderService _orderService;

        private readonly IShippingService _shippingService;
 
        private readonly IMapper _mapper;


        public OrderComposerService(
            IOrderService orderService,
            IShippingService shippingService,
            IMapper mapper
        )
        {
            _orderService = orderService;
            _shippingService = shippingService;
            _mapper = mapper;
        }

        public async Task<OrderSummaryViewModel> GetOrderSummary(Guid id)
        {
            var billingAddress = await _shippingService.GetOrderBillingAddressAsync(id);
            var shippingAddress = await _shippingService.GetOrderShippingAddressAsync(id);
            var orderItems = await _orderService.GetOrderPurchasedItems(id);

            return new OrderSummaryViewModel 
            {
                BillingAddress = billingAddress,
                ShippingAddress = shippingAddress,
                OrderItems = _mapper.Map<IList<OrderItemSummaryViewModel>>(orderItems)
            };
        }
    }

    public interface IOrderComposerService 
    {
        Task<OrderSummaryViewModel> GetOrderSummary(Guid id);
    }
}