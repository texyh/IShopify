using IShopify.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace IShopify.WebApi.Bootstrap
{
    /// <summary>
    /// This is the class for Registering and configuration authentication for the application.
    /// </summary>
    public static class AuthConfig
    {
        /// <summary>
        /// This adds authentication to the service collection
        /// </summary>
        /// <param name="services"></param>
        public static void AddApplicationAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateActor = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = AppSettingsProvider.Current.BaseUrl,
                        ValidAudience = AppSettingsProvider.Current.AppName,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(AppSettingsProvider.Current.TokenKey))
                    };

                });
        }
    }
}
