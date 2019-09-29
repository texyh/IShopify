using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Customer
{
    public interface ICustomerCacheService : ICustomerLookupService
    {
        void Add(Models.Customer customer);

        void Update(Models.Customer customer);

        void Remove(int id);
    }
}
