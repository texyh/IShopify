using System;
using System.Threading.Tasks;
using IShopify.Core.Orders.Messages;
using MassTransit;

namespace IShopify.ServiceBus.Consumers
{
    public class OrderEmailCommandConsumer : IConsumer<OrderConfirmedCommand>
    {
        public Task Consume(ConsumeContext<OrderConfirmedCommand> context)
        {
            var message = context.Message;
            Console.WriteLine(message.OrderId);

            return Task.CompletedTask;
        }
    }
}