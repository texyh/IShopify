using System;
using System.Collections.Generic;
using System.Text;
using IShopify.Core.MessageBus;
using MassTransit;

namespace IShopify.ServiceBus.BusProviders
{
    public class ActiveMqBusProvider : IBusProvider
    {
        public IBusControl CreateBus(params MessageRoute[] routes)
        {
            throw new NotImplementedException();
        }
    }
}
