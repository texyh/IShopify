﻿using IShopify.Core.Attributes.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Products.Models
{
    public class ProductAttributeValueEntity
    {
        public int ProductId { get; set; }

        public ProductEntity Product { get; set; }

        public int AttributeValueId { get; set; }

        public AttributeValueEntity AttributeValue { get; set; }
    }
}
