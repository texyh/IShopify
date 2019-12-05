using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Orders.Models
{
    public class OrderItem
    {
        public long Id { get; set; }

        public int ProductId { get; set; }

        public decimal UnitCost { get; set; }

        public decimal SubTotal
        {
            get
            {
                return Quantity * UnitCost;
            }
        }

        public int Quantity { get; set; }
    }
}
