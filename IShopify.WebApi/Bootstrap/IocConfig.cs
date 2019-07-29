﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using IShopify.Common.IocContainer;
using IShopify.Data.Bootstrap;
using IShopify.DomainServices.Bootstrap;
using IShopify.WebApiServices.Bootstrap;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IShopify.WebApi.Bootstrap
{
    /// <summary>
    /// 
    /// </summary>
    public static class IocConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceProvider AddDependencies(this IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<DomainServicesAutoFacModule>();
            containerBuilder.RegisterModule<DataAutofacModule>();
            containerBuilder.RegisterModule<ApiServicesAutofacModule>();

            containerBuilder.Populate(services);

            var builder = containerBuilder.Build();

            IocContainerProvider.Register(builder);

            return new AutofacServiceProvider(builder);

        }

    }
}