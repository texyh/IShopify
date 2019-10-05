using IShopify.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Common
{
    public interface IPermissionFactory
    {
        IProductPermissions CreateProductPermissions();
    }
}
