using Autofac;
using Autofac.Extensions.DependencyInjection;
using IShopify.Common;
using IShopify.Common.IocContainer;
using IShopify.Core.Config;
using IShopify.Core.Security;
using IShopify.Data.Bootstrap;
using IShopify.DomainServices.Bootstrap;
using IShopify.Framework.Bootstrap;
using IShopify.ServiceBus.Bootstrap;
using IShopify.WebApiServices.Bootstrap;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IShopify.WebApi.Bootstrap
{
    /// <summary>
    /// Registers The inversion of control
    /// </summary>
    public static class IocConfig
    {
        /// <summary>
        /// Adds Dependencies to Iservicecollection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceProvider AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<DomainServicesAutoFacModule>();
            containerBuilder.RegisterModule<DataAutofacModule>();
            containerBuilder.RegisterModule<ApiServicesAutofacModule>();
            containerBuilder.RegisterModule<FrameworkAutoFacModule>();
            containerBuilder.RegisterModule<ApiServiceBusAutofacModule>();
            containerBuilder.RegisterInstance(new AppSettings(configuration)).AsSelf().SingleInstance();

            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = AppSettingsProvider.Current.RedisSettings.Host;
            //    options.InstanceName = AppSettingsProvider.Current.RedisSettings.Instance;
            //    options.ConfigurationOptions = AppSettingsProvider.Current.RedisSettings.Options;
            //});

            services.AddScoped<IUserContext, WebUserContext>();
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

            containerBuilder.Populate(services);

            var builder = containerBuilder.Build();

            IocContainerProvider.Register(builder);

            return new AutofacServiceProvider(builder);

        }

    }
}
