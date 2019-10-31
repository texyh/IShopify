using IShopify.Core.Customer.Models;
using IShopify.Core.Data;
using IShopify.Core.Orders.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Orders.Models.Entity
{
    public class OrderEntity : IEntity<Guid>
    {
        public OrderEntity()
        {
            OrderItems = new List<OrderItemEntity>();
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

        public virtual ShippingAddressEntity ShippigAddress { get; set; }

        public virtual CustomerEntity Customer { get; set; }

        public virtual ICollection<OrderItemEntity> OrderItems { get; set; }
    }
}
