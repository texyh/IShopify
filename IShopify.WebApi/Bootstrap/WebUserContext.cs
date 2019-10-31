using IShopify.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IShopify.Core.Helpers;
using IShopify.Core.Customer.Models;

namespace IShopify.WebApi.Bootstrap
{
    /// <summary>
    /// User context of the web project
    /// </summary>
    public class WebUserContext : UserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private int? _userId;

        /// <summary>
        /// Constructor for the webusercontext
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public WebUserContext(IHttpContextAccessor httpContextAccessor) :base(null, null)
        {
            _httpContextAccessor = httpContextAccessor;

        }

        /// <summary>
        /// LoggedIn user Id
        /// </summary>
        public override int UserId
        {
            get
            {
                if(!_userId.HasValue || _userId.Value.IsDefault())
                {
                    var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
                    var userId = claimsPrincipal?.FindFirstValue(ClaimTypes.NameIdentifier);

                    _userId = userId.IsNullOrEmpty() ? default : int.Parse(userId);
                }

                return _userId.Value;
            }
        }

        /// <summary>
        /// LoggedIn User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected override Customer GetCustomer(int id)
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext?.User;

            return new Customer
            {
                Id = id,
                FirstName = claimsPrincipal?.FindFirstValue(ClaimTypes.GivenName), // TODO add family name to claims
                Email = claimsPrincipal?.FindFirstValue(ClaimTypes.Email)
            };
        }
    }
}
