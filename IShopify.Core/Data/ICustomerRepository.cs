using IShopify.Core.Customer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Data
{
    public interface ICustomerRepository : IDataRepository<CustomerEntity, int>
    {
        Task<CustomerEntity> GetAsync(int id, bool allowNull = false, bool isSummary=false);

        Task<CustomerEntity> GetCustomerWithAuthProfile(int id, bool allowNull = false);
    }
}
