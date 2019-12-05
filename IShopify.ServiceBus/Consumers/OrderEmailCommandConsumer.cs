

using System;
using System.Threading.Tasks;
using MassTransit;

public class OrderEmailCommandConsumer : IConsumer<OrderConfirmedCommand>
{
    public Task Consume(ConsumeContext<OrderConfirmedCommand> context)
    {
        var message = context.Message;
        Console.WriteLine(message.OrderId);

        return Task.CompletedTask;
    }
}