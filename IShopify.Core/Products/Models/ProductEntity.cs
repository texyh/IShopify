﻿using IShopify.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Products.Models
{
    public class ProductEntity : IEntity<int>
    {
        public ProductEntity()
        {
            ProductCategories = new List<ProductCategoryEntity>();
            Reviews = new List<ReviewEntity>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DisCountedPrice { get; set; }

        public string Image { get; set; }

        public string Image2 { get; set; }

        public int Display { get; set; }

        public DateTime? DeleteDateUtc {get; set;}

        public virtual ICollection<ProductCategoryEntity> ProductCategories { get; set; }

        public virtual ICollection<ProductAttributeValueEntity> ProductAttributeValues { get; set; }

        public virtual ICollection<ReviewEntity> Reviews { get; set; }
    } 
}
