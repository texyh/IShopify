using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Customer.Models
{
    public class SaveCustomerViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }
        
        public string DayPhone { get; set; }

        public string EveningPhone { get; set; }

        public string MobilePhone { get; set; }
    }
}
