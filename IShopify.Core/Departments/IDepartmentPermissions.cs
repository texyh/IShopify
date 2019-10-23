using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Departments
{
    public interface IDepartmentPermissions
    {
        bool CanEdit { get; }

        bool CanCreate { get; }

        bool CanView { get; }
    }
}
