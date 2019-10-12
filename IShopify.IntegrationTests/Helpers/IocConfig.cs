using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Autofac;
using IShopify.Data;
using IShopify.Data.Bootstrap;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IShopify.Core.Helpers;
using IShopify.Common;
using IShopify.DomainServices.Bootstrap;
using AutoMapper;

namespace IShopify.IntegrationTests.Helpers
{
    public static class IocConfig
    {
        private static string _projectBasePath;

        private static IContainer _container;

        public static IContainer Register()
        {
            if(_container.IsNotNull())
            {
                return _container;
            }

            var container = new ContainerBuilder();
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(ProjectBaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = configBuilder.Build();
            AppSettingsProvider.Register(new Core.Config.AppSettings(configuration));

            container.RegisterModule<DataAutofacModule>();
            container.RegisterModule<DomainServicesAutoFacModule>();

            container.Register(c => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataMapperProfile>();
            })).AsSelf().SingleInstance();

            container.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>().InstancePerLifetimeScope();

            container.RegisterInstance(new IShopifyDbContext(GetDbOptions()))
                     .AsSelf()
                     .SingleInstance();

            _container = container.Build();

            return _container;
        }

        private static DbContextOptions<IShopifyDbContext> GetDbOptions()
        {
            var builder = new DbContextOptionsBuilder<IShopifyDbContext>();
            builder.UseNpgsql(AppSettingsProvider.Current.IshopifyDB);

            return builder.Options;
        }

        public static string ProjectBaseDirectory
        {
            get
            {
                if (_projectBasePath.IsNullOrEmpty())
                {
                    var baseDir = Directory.GetCurrentDirectory();
                    var splitter = new string[] { @"\bin" };

                    _projectBasePath = baseDir.Split(splitter, StringSplitOptions.RemoveEmptyEntries)[0];
                }

                return _projectBasePath;
            }
        }
    }
}
