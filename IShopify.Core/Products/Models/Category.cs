using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Products.Models
{
    public class Category
    {
        public int Id { get; set; }

        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Department Department { get; set; }
    }
}
