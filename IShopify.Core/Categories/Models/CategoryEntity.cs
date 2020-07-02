using IShopify.Core.Data;
using IShopify.Core.Departments;
using IShopify.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Categories.Models
{
    public class CategoryEntity : IEntity<int>
    {
        public CategoryEntity()
        {
            ProductCategories = new List<ProductCategoryEntity>();
        }

        public int Id { get; set; }

        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual DepartmentEntity Department { get; set; }

        public virtual ICollection<ProductCategoryEntity> ProductCategories { get; set; }

        public DateTime? DeleteDateUtc {get; set;}
    }
}
