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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IShopify.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerService"></param>
        public CustomerController(ICustomerService customerService, 
            IAccountService accountService, IServiceProvider serviceProvider)
        {
            _customerService = customerService;
            _accountService = accountService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Customer> UpdateCustomer([FromBody] SaveCustomerViewModel model)
        {
            return await _customerService.UpdateCustomerAsync(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Customer> Get()
        {
            return await _customerService.GetAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("address")]
        public async Task<Customer> UpdateCustomerAddress([FromBody] SaveCustomerAddressViewModel model)
        {
            return await _customerService.UpdateCustomerAddressAsync(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="creditCard"></param>
        /// <returns></returns>
        [HttpPut("creditcard")]
        public async Task UpdateCustomerCreditCard(string creditCard)
        {
            await _customerService.UpdateCustomerCreditCardAsync(creditCard);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AuthenticationResponse> RegisterCustomerAsync([FromBody]CustomerRegistrationViewModel model)
        {
            return  await _accountService.RegisterCustomerAsync(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<AuthenticationResponse> Login([FromBody] CustomerLoginViewModel model)
        {
            return await _accountService.LoginCustomerAsync(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("facebook")]
        public async Task<AuthenticationResponse> LoginByFaceBook(string token)
        {
            throw new NotImplementedException();
        }

    }
}