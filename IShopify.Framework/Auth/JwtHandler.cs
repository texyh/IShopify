﻿using IShopify.Core.Config;
using IShopify.Core.Customer.Models;
using Microsoft.Extensions.Options;
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
        private readonly AppSettings _appSettings;
        private const int MinutesToExpire = 24 * 60; // One day;

        public JwtHandler(IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;
        }

        public string CreateAccessToken(Customer customer)
        {
            var tokenKey = _appSettings.TokenKey;
            var baseUrl = _appSettings.BaseUrl;
            var appName = _appSettings.AppName;
            
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
