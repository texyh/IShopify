using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Customer
{
    public interface ICustomerLookupService
    {
        Task<Models.Customer> GetCustomerAsync(int id);

        Task<IList<Models.Customer>> GetCustomersAsync(IEnumerable<int> Ids);
    }
}
