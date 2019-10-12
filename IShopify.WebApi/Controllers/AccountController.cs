using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountService"></param>
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
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