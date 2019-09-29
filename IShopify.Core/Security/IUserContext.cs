using IShopify.Core.Customer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Security
{
    public interface IUserContext
    {
        int UserId { get; }

        string Email { get; }

        string DisplayName { get; }

        bool IsAnonymous { get; }

        // CustomerRole[] Roles { get; }
    }
}
