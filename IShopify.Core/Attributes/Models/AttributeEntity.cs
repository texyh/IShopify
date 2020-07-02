using IShopify.Core.Data;
using IShopify.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Attributes.Models
{
    public class AttributeEntity : IEntity<int>
    {
        public AttributeEntity()
        {
            Values = new List<AttributeValueEntity>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AttributeValueEntity> Values { get; set; }

        public DateTime? DeleteDateUtc {get; set;}
    }
}
