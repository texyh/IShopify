using Autofac;
using IShopify.Core.MessageBus;
using IShopify.ServiceBus.BusProviders;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.ServiceBus.Bootstrap
{
    public static class BusConfig
    {
        public static IBusControl ConfigureApiServiceBus(IComponentContext context)
        {
            var busProvider = context.ResolveKeyed<IBusProvider>("rabbitmq"); // move to enum
            return busProvider.CreateBus();
        }

        public static IBusControl ConfigureBackgroundServiceBus(IComponentContext context)
        {
            var busProvider = context.ResolveKeyed<IBusProvider>("rabbitmq"); // move to enum
            return busProvider.CreateBus(MessageRoute.ShortRunning);
        }
    }
}
