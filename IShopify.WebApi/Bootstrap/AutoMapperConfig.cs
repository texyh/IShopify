using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IShopify.Data.Bootstrap;
using IShopify.DomainServices.Bootstrap;
using IShopify.Framework.Bootstrap;
using IShopify.WebApiServices.Bootstrap;
using Microsoft.Extensions.DependencyInjection;

namespace IShopify.WebApi.Bootstrap
{
    /// <summary>
    /// This class is used Initiaze Automapper
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Adds Automapper to IServiceCollection
        /// </summary>
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(DomainServicesMapperProfile), 
                typeof(ApiServiceMapperProfile),
                typeof(FrameworkMapperProfile), 
                typeof(DataMapperProfile));
        }
    }
}
