using IShopify.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Products.Models
{
    public class CategoryEntity : IEntity
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
    }
}
