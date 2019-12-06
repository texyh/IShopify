using IShopify.Core.MessageBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Orders.Messages
{
    public class OrderConfirmedCommand : Command
    {
        public OrderConfirmedCommand(int userId, Guid orderId) : base(userId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }
    }
}
