using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Products.Models
{
    public class SaveProductModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DisCountedPrice { get; set; }

        public string Image { get; set; }

        public string Image2 { get; set; }

        public int Display { get; set; }
    }
}
