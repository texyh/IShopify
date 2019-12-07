using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Categories
{
    public interface ICategoryPermissions
    {
        bool CanCreate { get; }

        bool CanEdit { get; }

        bool CanView { get; }

        bool CanDelete { get; }
    }
}
