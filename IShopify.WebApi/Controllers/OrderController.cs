

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IShopify.Core.Orders;
using Microsoft.AspNetCore.Mvc;

///

///
[Route("orders")]
[ApiController]
public class OrderController : ControllerBase  
{
    private readonly IOrderService _orderService;
    private readonly IShippingService _shippingService;


    ///
    ///
    public OrderController(
        IOrderService orderService,
        IShippingService shippingService
    ) 
    {
        _orderService = orderService;
        _shippingService = shippingService;
    }

    ///
    ///
    [HttpPost]
    public async Task<Guid> CreateAsync([FromBody] IList<SaveOrderItemViewModel> orderItems) 
    {
        return await _orderService.CreateAsync(orderItems);
    }

    ///
    ///
    [HttpGet("billing_address")]
    public async Task<IList<OrderAddressViewModel>> GetCustomerBillingAddressesAsync() 
    {
        return await _shippingService.GetAllCustomerBillingAddressAsync();
    }

    ///
    ///
    [HttpGet("shipping_address")]
    public async Task<IList<OrderAddressViewModel>> GetCustomerShippingAddressesAsync()
    {
        return await _shippingService.GetAllCustomerShippingAddressAsync();
    }

    ///
    ///
    [HttpPost("{orderId:Guid}/billing_address")]
    public async Task SaveBillingAddressAsync(Guid orderId, [FromBody]SaveOrderAddressViewModel address)
    {
        await _shippingService.SaveBillingAddressAsync(orderId, address);
    }

    ///
    ///
    [HttpPost("{orderId:Guid}/billing_address/{addressId:int}")]
    public async Task SaveBillingAddressAsync(Guid orderId, int addressId)
    {
        await _shippingService.SaveOrderAddressAsync(orderId, addressId);
    }

    [HttpPost("{orderId:Guid}/shipping_address/{addressId:int}")]
    public async Task SaveShippingAddressAsync(Guid orderId, int addressId)
    {
        await _shippingService.SaveOrderAddressAsync(orderId, addressId);
    }

    [HttpPost("{orderId:Guid}/shipping_address")]
    public async Task SaveShippingAddressAsync(Guid orderId, [FromBody]SaveOrderAddressViewModel address) 
    {
        await _shippingService.SaveShippingAddressAsync(orderId, address);
    }

    [HttpPut]
    public async Task ConfirmOrder(Guid id) 
    {
        await _orderService.ConfirmOrder(id);
    }
}