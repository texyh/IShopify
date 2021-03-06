﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.WebApiServices.ViewModels.Products
{
    public class ProductSummaryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DisCountedPrice { get; set; }

        public string Image { get; set; }
    }
}
