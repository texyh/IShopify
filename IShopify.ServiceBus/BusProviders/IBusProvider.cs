using IShopify.Core.MessageBus;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.ServiceBus.BusProviders
{
    public interface IBusProvider
    {
        IBusControl CreateBus(params MessageRoute[] routes);
    }
}
