using IShopify.Core.Categories.Models;
using IShopify.Core.Data;
using System.Collections.Generic;

namespace IShopify.Core.Departments
{
    public class DepartmentEntity : IEntity<int>
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
