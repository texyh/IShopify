using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Orders.Models
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
            OrderStatus = OrderStatus.Pending;
        }

        public Guid Id { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ShippingAddressId { get; set; }

        public int CustomerId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public ShippingMethod ShippingMethod { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public DateTime? ShippedOn { get; set; }

        public  IList<OrderItem> OrderItems { get; set; }
    }
}
