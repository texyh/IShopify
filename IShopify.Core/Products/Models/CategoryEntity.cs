using IShopify.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Products.Models
{
    public class CategoryEntity : IEntity
    {
        public int Id { get; set; }

        public int DepartmentID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
