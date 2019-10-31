using IShopify.Core.Customer.Models;
using IShopify.Core.Data;
using IShopify.Core.Orders.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Orders.Models.Entities
{
    public class ShippingAddressEntity : Address, IEntity<int>
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CustomerId { get; set; }

        public int OrderId { get; set; }

        public OrderEntity Order { get; set; }

        public virtual CustomerEntity Customer { get; set; }

    }
}
