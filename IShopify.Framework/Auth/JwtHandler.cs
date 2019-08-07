using IShopify.Core.Customer.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IShopify.Framework.Auth
{
    public class JwtHandler : IJwtHandler
    {
        private const int MinutesToExpire = 24 * 60; // One day;

        public string CreateAccessToken(Customer customer)
        {
            var tokenKey = "2SDMJ6fOJJ8kZsURfRjIDMJI1ZYqv6ZLHCBlHHNAInI"; // TODO get from config
            var baseUrl = "https://localhost:5001/"; // TODO get from config
            var appName = "app_Name";
            
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, customer.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, customer.Id.ToString()),
                new Claim(ClaimTypes.Name, customer.Name),
                new Claim(ClaimTypes.Email, customer.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddMinutes(MinutesToExpire);
            var jwt = CreateSecurityToken(claims, expiry, signingCredentials, issuer: baseUrl, audience: appName);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }

        private JwtSecurityToken CreateSecurityToken(IEnumerable<Claim> claims, DateTime expiry, SigningCredentials credentials, string issuer, string audience)
            => new JwtSecurityToken(claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiry,
                signingCredentials: credentials,
                issuer: issuer,
                audience: audience);
    }

    public interface IJwtHandler
    {
        string CreateAccessToken(Customer customer);

    }
}
