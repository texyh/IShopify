using IShopify.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Products.Models
{
    public class DepartmentEntity : IEntity
    {
        public DepartmentEntity()
        {
            Categories = new List<CategoryEntity>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<CategoryEntity> Categories { get; set; }
    }
}
