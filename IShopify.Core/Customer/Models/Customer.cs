using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Customer.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        
        public string CreditCard { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string ShippingRegionId { get; set; }

        public string DayPhone { get; set; }

        public string EveningPhone { get; set; }

        public string MobilePhone { get; set; }

    }
}
