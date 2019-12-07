using IShopify.Core.Attributes;
using IShopify.Core.Categories;
using IShopify.Core.Common;
using IShopify.Core.Departments;
using IShopify.Core.Departments.Models;
using IShopify.Core.Products;
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

        public IDepartmentPermissions CreateDepartmentPermissions()
        {
            return new DepartmentPermission();
        }

        public ICategoryPermissions CreateCategoryPermission()
        {
            throw new NotImplementedException();
        }

        public IAttributePermissions CreateAttributePermissions()
        {
            throw new NotImplementedException();
        }
    }
}
