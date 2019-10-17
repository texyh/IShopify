using IShopify.Core.MessageBus;
using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IShopify.ServiceBus
{
    public class MessageBus : IMessageBus
    {
        private readonly IBus _bus;

        public MessageBus(IBus bus)
        {
            _bus = bus;
        }

        public Task PublishAsync<TMessage>(TMessage message, CancellationToken cancelationToken) where TMessage : class, IMessage
        {
            return _bus.Publish(message, cancelationToken);
        }
    }
}
