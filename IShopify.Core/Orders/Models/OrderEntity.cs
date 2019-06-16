using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Orders.Models
{
    public class OrderEntity
    {
        public int Id { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ShippedOn { get; set; }
    }
}
