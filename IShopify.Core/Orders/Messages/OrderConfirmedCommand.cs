using IShopify.Core.MessageBus;
using System;

public class OrderConfirmedCommand : Command {
    public OrderConfirmedCommand (int userId, Guid orderId) 
        : base (userId) 
    {
        OrderId = orderId;
    }

    public Guid OrderId { get; set; }
}