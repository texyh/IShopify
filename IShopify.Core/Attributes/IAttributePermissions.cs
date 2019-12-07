using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Attributes
{
    public interface IAttributePermissions
    {
        bool CanEdit { get; }

        bool CanCreate { get; }

        bool CanView { get; }
    }
}
