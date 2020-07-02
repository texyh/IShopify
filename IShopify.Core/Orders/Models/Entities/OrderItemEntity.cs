using IShopify.Core.Data;
using IShopify.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Orders.Models.Entity
{
    public class OrderItemEntity : IEntity<long>
    {
        public long Id { get; set; }

        public int ProductId { get; set; }

        public Guid OrderId { get; set; }

        public decimal UnitCost { get; set; }

        public int Quantity { get; set; }

        public DateTime? DeleteDateUtc {get; set;}


        public virtual ProductEntity Product { get; set; }

        public virtual OrderEntity Order { get; set; }
    }
}
