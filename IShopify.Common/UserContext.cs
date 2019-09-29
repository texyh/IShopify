using IShopify.Core.Customer;
using IShopify.Core.Customer.Models;
using IShopify.Core.Helpers;
using IShopify.Core.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Common
{
    public class UserContext : IUserContext
    {
        private Customer _user;
        private int? _userId;
        private CustomerRole[] _userRoles;
        private readonly ICustomerLookupService _customerLookupService;

        public UserContext(int? userId, ICustomerLookupService customerLookupService)
        {
            _userId = userId;
            _customerLookupService = customerLookupService;
        }

        private UserContext(Customer user)
        {
            _user = user;
        }

        public virtual int UserId => _userId ?? (_userId = default).Value;

        public virtual string Email => GetCustomer().Email;

        public virtual string DisplayName => GetCustomer().Name;

        public bool IsAnonymous => UserId.IsDefault();

        // public virtual CustomerRole[] Roles => throw new NotImplementedException(); // TODO revisit

        protected virtual Customer GetCustomer(int id)
        {
            ArgumentGuard.NotNull(_customerLookupService, nameof(_customerLookupService));

            return _customerLookupService.GetCustomerAsync(id).GetAwaiter().GetResult();
        }

        private Customer GetCustomer()
        {
            if(_user.IsNotNull())
            {
                return _user;
            }

            if (UserId.IsDefault())
            {
                return _user = new Customer
                {
                    Id = default,
                    Name = string.Empty,
                    Email = string.Empty,
                };
            }

            return _user = GetCustomer(UserId);
        }

        public static IUserContext Create(int userId, string email, string displayName)
        {
            var user = new Customer
            {
                Id = userId,
                Email = email,
                Name = displayName
            };

            return new UserContext(user);
        }
    }
}
