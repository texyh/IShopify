

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IShopify.Core.MessageBus;
using IShopify.Core.Orders;
using IShopify.Core.Orders.Messages;
using IShopify.Core.Products.Messages;
using IShopify.Core.Security;
using Microsoft.AspNetCore.Mvc;

namespace IShopify.WebApi.Controllers {
    
    /// <summary>
    /// 
    /// </summary>
    [Route("orders")]
    [ApiController]
    public class OrderController : ControllerBase  
    {
        private readonly IOrderService _orderService;

        private readonly IShippingService _shippingService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderService"></param>
        /// <param name="shippingService"></param>
        /// <param name="userContext"></param>
        /// <param name="messageBus"></param>
        public OrderController(
            IOrderService orderService,
            IShippingService shippingService,
            IUserContext userContext,
            IMessageBus messageBus
        ) 
        {
            _orderService = orderService;
            _shippingService = shippingService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Guid> CreateAsync([FromBody] IList<SaveOrderItemViewModel> orderItems) 
        {
            return await _orderService.CreateAsync(orderItems);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("/customers/billing_address")]
        public async Task<IList<OrderAddressViewModel>> GetCustomerBillingAddressesAsync() 
        {
            return await _shippingService.GetAllCustomerBillingAddressAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("~/customers/shipping_address")]
        public async Task<IList<OrderAddressViewModel>> GetCustomerShippingAddressesAsync()
        {
            return await _shippingService.GetAllCustomerShippingAddressAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPost("{orderId:Guid}/billing_address")]
        public async Task SaveBillingAddressAsync(Guid orderId, [FromBody]SaveOrderAddressViewModel address)
        {
            await _shippingService.SaveBillingAddressAsync(orderId, address);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        [HttpPost("{orderId:Guid}/billing_address/{addressId:int}")]
        public async Task SaveBillingAddressAsync(Guid orderId, int addressId)
        {
            await _shippingService.SaveOrderAddressAsync(orderId, addressId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        [HttpPost("{orderId:Guid}/shipping_address/{addressId:int}")]
        public async Task SaveShippingAddressAsync(Guid orderId, int addressId)
        {
            await _shippingService.SaveOrderAddressAsync(orderId, addressId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPost("{orderId:Guid}/shipping_address")]
        public async Task SaveShippingAddressAsync(Guid orderId, [FromBody]SaveOrderAddressViewModel address) 
        {
            await _shippingService.SaveShippingAddressAsync(orderId, address);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:Guid}/confirm")]
        public async Task ConfirmOrder(Guid id) 
        {
            await _orderService.ConfirmOrder(id);
        }


        // [HttpGet("{id:Guid}/summary")]
        // public async Task GetOrderSummary(Guid id) 
        // {

        // }
    }

}
