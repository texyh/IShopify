using Autofac;
using Autofac.Extensions.DependencyInjection;
using IShopify.Common.IocContainer;
using IShopify.Data.Bootstrap;
using IShopify.DomainServices.Bootstrap;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IShopify.WebApi.Bootstrap
{
    public static class IocConfig
    {

        public static IServiceProvider AddDependencies(this IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<DomainServicesAutoFacModule>();
            containerBuilder.RegisterModule<DataAutofacModule>();
            containerBuilder.Populate(services);

            var builder = containerBuilder.Build();

            IocContainerProvider.Register(builder);

            return new AutofacServiceProvider(builder);

        }

    }
}
