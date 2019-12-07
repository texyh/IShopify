using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IShopify.Common;
using IShopify.Core.Config;
using IShopify.Core.Customer;
using IShopify.Core.Customer.Models;
using IShopify.Core.Security;
using IShopify.DomainServices.Customer;
using IShopify.Framework.Auth;
using IShopify.Framework.Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IShopify.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing customers
    /// </summary>
    [Route("customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        /// <summary>
        ///  constructor for customer controller
        /// </summary>
        /// <param name="customerService"></param>
        /// <param name="accountService"></param>
        public CustomerController(ICustomerService customerService, 
            IAccountService accountService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Endpoint for updating customer details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Customer> UpdateCustomer([FromBody] SaveCustomerViewModel model)
        {
            return await _customerService.UpdateCustomerAsync(model);
        }

        /// <summary>
        /// Endpoint for getting logged customer details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Customer> Get()
        {
            return await _customerService.GetAsync();
        }

        /// <summary>
        /// Endpoint for updated customer address
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("address")]
        public async Task<Customer> UpdateCustomerAddress([FromBody] SaveCustomerAddressViewModel model)
        {
            return await _customerService.UpdateCustomerAddressAsync(model);
        }

    }
}