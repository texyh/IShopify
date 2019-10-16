using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using IShopify.Core.Config;
using IShopify.Core.MessageBus;
using IShopify.ServiceBus.Consumers;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace IShopify.ServiceBus.BusProviders
{

    public class RabbitMqBusProvider : IBusProvider
    {
        private readonly ILifetimeScope _context;

        private readonly AppSettings _appSettings;

        private const string ShortRunningKey = "short_running";

        public RabbitMqBusProvider(ILifetimeScope context, AppSettings appSettings)
        {
            _context = context;
            _appSettings = appSettings;
        }

        public IBusControl CreateBus(params MessageRoute[] routes)
        {
            return Bus.Factory.CreateUsingRabbitMq(configurator =>
            {
                var host = configurator.Host(new Uri(_appSettings.QueueSettings.Url), h =>
                {
                    h.Username(_appSettings.QueueSettings.UserName);
                    h.Password(_appSettings.QueueSettings.Password);
                });

                foreach (var route in routes)
                {
                    switch (route)
                    {
                        case MessageRoute.ShortRunning:
                            ConfigureShortRunningReceiveEndpoint(configurator, host, _appSettings.QueueSettings);
                            break;

                        default:
                            throw new InvalidOperationException($"Unsupported route '{route}'");
                    }
                }
            });
        }

        private void ConfigureShortRunningReceiveEndpoint(IRabbitMqBusFactoryConfigurator configurator,
            IRabbitMqHost host, QueueSettings queueConfig)
        {
            var shortRunningQ = $"{queueConfig.QueueNamePrefix}{ShortRunningKey}_q";

            configurator.ReceiveEndpoint(host, shortRunningQ, ec =>
            {
                ec.PrefetchCount = (ushort)queueConfig.PrefetchCount;

                ec.Consumer<ProductCreatedCommandConsumer>(_context);
            });
        }
    }
}
