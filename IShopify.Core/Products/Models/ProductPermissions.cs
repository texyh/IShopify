using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Products.Models
{
    public class ProductPermissions : IProductPermissions
    {
        private readonly Lazy<bool> _canCreate;

        public ProductPermissions()
        {
            _canCreate = new Lazy<bool>(IsCanCreate());
        }

        private bool canCreate => _canCreate.Value;

        private bool IsCanCreate()
        {
            return true;
        }
    }
}
