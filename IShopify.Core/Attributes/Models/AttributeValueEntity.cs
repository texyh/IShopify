using IShopify.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Attributes.Models
{
    public class AttributeValueEntity
    {
        public AttributeValueEntity()
        {
            ProductAttributeValues = new List<ProductAttributeValueEntity>();
        }
        public int Id { get; set; }

        public int AttributeId { get; set; }

        public string Value { get; set; }

        public virtual AttributeEntity Attribute { get; set; }

        public virtual ICollection<ProductAttributeValueEntity> ProductAttributeValues { get; set; }

    }
}
