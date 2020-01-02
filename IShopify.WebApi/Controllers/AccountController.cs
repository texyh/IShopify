using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IShopify.Core.Customer;
using IShopify.Core.Customer.Models;
using IShopify.Framework.Auth;
using IShopify.Framework.Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IShopify.WebApi.Controllers
{
    /// <summary>
    /// Constructor for manage customer account
    /// </summary>
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        private readonly ICustomerService _customerService;

        private readonly ICustomerLookupService _customerLookupService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountService"></param>
        public AccountController(IAccountService accountService, ICustomerService customerService,
            ICustomerLookupService customerLookupService)
        {
            _accountService = accountService;
            _customerService = customerService;
            _customerLookupService = customerLookupService;
        }

        /// <summary>
        /// Endpoint for registering a customer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<AuthenticationResponse> RegisterCustomerAsync([FromBody]CustomerRegistrationViewModel model)
        {
            return await _accountService.RegisterCustomerAsync(model);
        }

        /// <summary>
        /// Endpoint for logging In
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<AuthenticationResponse> Login([FromBody] CustomerLoginViewModel model)
        {
            return await _accountService.LoginCustomerAsync(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("_resetPassword")]
        [AllowAnonymous]
        public async Task PasswordResetRequest(string email) 
        {
            await _customerService.PasswordResetRequestAsync(email);
        }

        /// <summary>
        /// Endpoint for logging in with facebook
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("facebook")]
        [AllowAnonymous]
        public Task<AuthenticationResponse> LoginByFaceBook(string token)
        {
            throw new NotImplementedException(); // TODO implement
        }
    }
}