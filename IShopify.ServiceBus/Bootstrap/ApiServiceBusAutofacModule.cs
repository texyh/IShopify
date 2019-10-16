using Autofac;
using IShopify.Core.MessageBus;
using IShopify.ServiceBus.BusProviders;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.ServiceBus.Bootstrap
{
    public class ApiServiceBusAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MessageBus>().As<IMessageBus>().SingleInstance();

            builder
                .Register(context => BusConfig.ConfigureApiServiceBus(context))
                .SingleInstance()
                .As<IBus>()
                .As<IBusControl>();

            builder.RegisterType<RabbitMqBusProvider>()
                .Keyed<IBusProvider>("rabbitmq")
                .SingleInstance();
        }
    }
}
