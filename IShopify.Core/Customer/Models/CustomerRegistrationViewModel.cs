using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Customer.Models
{
    public class CustomerRegistrationViewModel
    {
        public string  Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string LastName { get; set; }
    }
}
