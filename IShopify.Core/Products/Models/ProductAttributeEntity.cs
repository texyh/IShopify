using IShopify.Core.Attributes.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Products.Models
{
    public class ProductAttributeEntity
    {
        public int ProductId { get; set; }

        public ProductEntity Product { get; set; }

        public int AttributeValueId { get; set; }

        public AttributeEntity AttributeEntity { get; set; }
    }
}
