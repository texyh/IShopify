using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Products.Models
{
    public interface IProductPermissions
    {
        bool CanCreate { get;  }

        bool CanEdit { get;  }

        bool CanView { get;  }
    }
}
