using Autofac;
using AutoMapper;
using dotenv.net;
using dotenv.net.DependencyInjection.Infrastructure;
using IShopify.Common;
using IShopify.Common.IocContainer;
using IShopify.Core.Config;
using IShopify.Data.Bootstrap;
using IShopify.DomainServices.Bootstrap;
using IShopify.ServiceBus.Bootstrap;
using MassTransit;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;
using Topshelf;

namespace IShopify.BackgroundProcessor
{
    public class Program
    {
        static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = configBuilder.Build();

            containerBuilder.RegisterModule<BackgroundServiceBusAutofacModule>();
            containerBuilder.RegisterModule<DataAutofacModule>();
            containerBuilder.RegisterModule<DomainServicesAutoFacModule>();

            DotEnv.Config();

            var appSettings = new AppSettings(configuration);

            containerBuilder.RegisterInstance(appSettings).AsSelf().SingleInstance();
            AppSettingsProvider.Register(appSettings);

            containerBuilder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainServicesMapperProfile>();
            }));

            containerBuilder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().SingleInstance();

            var container = containerBuilder.Build();

            IocContainerProvider.Register(container);

            HostFactory.Run(c =>
            {
                c.Service<IBusControl>(s =>
                {
                    s.ConstructUsing(x => container.Resolve<IBusControl>());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                    s.WhenShutdown(service => service.Stop());
                    s.WhenPaused(service => service.Stop());
                    s.WhenContinued(service => service.Start());
                });

                c.RunAsLocalService();
                c.StartAutomatically();
                c.SetServiceName("IShopify Background Processor");
                c.SetDisplayName("IShopify Processor");
                c.SetServiceName("IShopify.Processor");
                c.SetDescription("Background processor for IShopify jobs");
            });
        }
    }
}
