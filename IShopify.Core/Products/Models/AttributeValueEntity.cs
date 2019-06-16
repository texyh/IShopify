using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Products.Models
{
    public class AttributeValueEntity
    {
        public int Id { get; set; }

        public int AttributeId { get; set; }

        public string Value { get; set; }
    }
}
