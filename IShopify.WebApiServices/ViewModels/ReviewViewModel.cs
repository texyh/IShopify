using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.WebApiServices.ViewModels
{
    public class ReviewViewModel
    {
        public string Name { get; set; }

        public string Review { get; set; }

        public int Rating { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
