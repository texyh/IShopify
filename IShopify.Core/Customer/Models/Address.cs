using IShopify.Core.Customer.Models;
using IShopify.Core.Orders.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Customer.Models
{
    public abstract class Address
    {
        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string PhoneNumber { get; set; }

        public string PostalCode { get; set; }

        // TODO Add more props
    }
}
