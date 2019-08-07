using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Customer.Models
{
    public class SaveCustomerAddressViewModel
    {
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public int ShippingRegionId { get; set; }
    }
}
