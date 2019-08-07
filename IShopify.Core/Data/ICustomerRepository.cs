using IShopify.Core.Customer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Data
{
    public interface ICustomerRepository : IDataRepository<CustomerEntity>
    {
    }
}
