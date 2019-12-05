

using System.Collections.Generic;
using System.Linq;
using IShopify.Core.Orders;

public class OrderSummaryViewModel 
{
    public OrderAddressViewModel BillingAddress {get; set;}
    public OrderAddressViewModel ShippingAddress {get; set;}
    public IList<OrderItemSummaryViewModel> OrderItems {get; set;}
    public decimal SubTotal => OrderItems.Sum(x => x.Total);
    public decimal ShippingCost {get; set;}
    public decimal Tax {get; set;}
    public decimal Total => SubTotal + ShippingCost + Tax;
}