using IShopify.Core.Attributes;
using IShopify.Core.Categories;
using IShopify.Core.Departments;
using IShopify.Core.Products;
using IShopify.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Common
{
    public interface IPermissionFactory
    {
        IProductPermissions CreateProductPermissions();

        IDepartmentPermissions CreateDepartmentPermissions();

        ICategoryPermissions CreateCategoryPermission();

        IAttributePermissions CreateAttributePermissions();
    }
}
