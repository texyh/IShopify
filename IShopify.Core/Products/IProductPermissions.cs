using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Products
{
    public interface IProductPermissions
    {
        bool CanCreate { get;  }

        bool CanEdit { get;  }

        bool CanView { get;  }
    }
}
