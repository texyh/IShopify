using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.WebApiServices.ViewModels.Products
{
    public class ProductLocationViewModel
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
}
