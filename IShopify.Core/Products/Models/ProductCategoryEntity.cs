using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Products.Models
{
    public class ProductCategoryEntity
    {
        public int ProductId { get; set; }

        public virtual ProductEntity Product { get; set; }

        public int CategoryId { get; set; }

        public virtual CategoryEntity Category { get; set; }
    }
}
