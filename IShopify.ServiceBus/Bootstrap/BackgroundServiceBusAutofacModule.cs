using Autofac;
using IShopify.Core.MessageBus;
using IShopify.ServiceBus.BusProviders;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace IShopify.ServiceBus.Bootstrap
{
    public class BackgroundServiceBusAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder container)
        {
            container.RegisterConsumers(Assembly.GetExecutingAssembly());

            container
                .Register(context => BusConfig.ConfigureBackgroundServiceBus(context))
                .SingleInstance()
                .As<IBus>()
                .As<IBusControl>();

            container.RegisterType<RabbitMqBusProvider>()
                .Keyed<IBusProvider>("rabbitmq")
                .SingleInstance();

            container.RegisterType<MessageBus>()
                .As<IMessageBus>()
                .SingleInstance();
        }
    }
}
