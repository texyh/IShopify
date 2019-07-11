using Autofac;
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
        }
    }
}
