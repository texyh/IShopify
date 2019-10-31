﻿using IShopify.Core.Customer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Customer
{
    public interface ICustomerService
    {
        Task<Core.Customer.Models.Customer> UpdateCustomerAsync(SaveCustomerViewModel model);

        Task<Core.Customer.Models.Customer> GetAsync();

        Task<Core.Customer.Models.Customer> UpdateCustomerAddressAsync(SaveCustomerAddressViewModel model);

    }
}
