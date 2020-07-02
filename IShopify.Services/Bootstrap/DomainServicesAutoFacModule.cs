using Autofac;
using FluentValidation;
using IShopify.Core.Common;
using IShopify.Core.Emails;
using IShopify.DomainServices.Common;
using IShopify.DomainServices.Validation;
using IShopify.Services.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IShopify.DomainServices.Bootstrap
{
    public class DomainServicesAutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = GetType().Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.GetInterfaces().Any(i => i.Name.EndsWith("Service")))
                .As(t => t.GetInterfaces().Where(i => i.Name.EndsWith("Service")))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<ValidatorFactory>()
                .As<Validation.IValidatorFactory>()
                .SingleInstance();

            builder.RegisterType<PermissionFactory>()
                .As<IPermissionFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TemplateLoader>()
                .As<ITemplateLoader>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmailSender>()
                .As<IEmailSender>()
                .InstancePerLifetimeScope();
        }
    }
}
