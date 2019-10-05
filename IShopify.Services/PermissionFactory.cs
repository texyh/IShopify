using IShopify.Core.Common;
using IShopify.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.DomainServices
{
    public class PermissionFactory : IPermissionFactory
    {
        public IProductPermissions CreateProductPermissions()
        {
            return new ProductPermissions();
        }
    }
}
