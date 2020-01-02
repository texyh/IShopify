using Autofac;
using IShopify.Core.Config;
using IShopify.Core.Emails;
using IShopify.Core.Framework;
using IShopify.Core.Framework.Logging;
using IShopify.Framework.Auth;
using IShopify.Framework.Logging;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using System.Net.Mail;

namespace IShopify.Framework.Bootstrap
{
    public class FrameworkAutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //var assembly = GetType().Assembly;

            //builder.RegisterAssemblyTypes(assembly)
            //    .Where(t => t.GetInterfaces().Any(i => i.Name.EndsWith("Service")))
            //    .As(t => t.GetInterfaces().Where(i => i.Name.EndsWith("Service")))
            //    .InstancePerLifetimeScope();

            builder.RegisterType<JwtHandler>()
                .As<IJwtHandler>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Logger>()
                .As<ILogger>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TemplateService>()
                .As<ITemplateService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RedisCacheService>()
                .As<IRedisCacheService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AccountService>()
                .As<IAccountService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CryptoService>()
                .As<ICryptoService>()
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

            builder.Register<IDistributedCache>(context =>
            {
                var appSettings = context.Resolve<AppSettings>();

                var options = new RedisCacheOptions
                {
                    Configuration = appSettings.RedisSettings.Host, // This is added here because of dev.
                    InstanceName = appSettings.RedisSettings.Instance,
                    ConfigurationOptions = appSettings.RedisSettings.Options
                };

                return new RedisCache(options);
            }).SingleInstance()
              .As<IDistributedCache>();

#if DEBUG
            builder.RegisterType<SmtpClient>().AsSelf().SingleInstance();
            builder.RegisterType<DevEmailService>().As<IEmailService>().InstancePerLifetimeScope();
#endif
        }
    }
}
