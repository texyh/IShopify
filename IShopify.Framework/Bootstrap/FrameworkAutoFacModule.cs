using Autofac;
using IShopify.Core.Config;
using IShopify.Core.Framework;
using IShopify.Core.Framework.Logging;
using IShopify.Framework.Auth;
using IShopify.Framework.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IShopify.Framework.Bootstrap
{
    public class FrameworkAutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = GetType().Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.GetInterfaces().Any(i => i.Name.EndsWith("Service")))
                .As(t => t.GetInterfaces().Where(i => i.Name.EndsWith("Service")))
                .InstancePerLifetimeScope();

            builder.RegisterType<JwtHandler>()
                .As<IJwtHandler>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RedisCacheService>()
                .As<IRedisCacheService>()
                .SingleInstance();

            builder.RegisterType<Logger>()
                .As<ILogger>()
                .InstancePerLifetimeScope();

            builder.Register<ILogProvider>(context =>
            {
                var appSettings = context.Resolve<AppSettings>();

                if(appSettings.LogTarget == LogTarget.Console)
                {
                    return new ConsoleLogProvider();
                }

                return new DataBaseLogProvider(appSettings);
            })
            .SingleInstance()
            .As<ILogProvider>();
                
        }
    }
}
