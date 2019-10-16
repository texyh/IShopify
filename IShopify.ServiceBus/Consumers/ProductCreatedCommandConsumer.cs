using IShopify.Core.Products.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.ServiceBus.Consumers
{
    public class ProductCreatedCommandConsumer : IConsumer<ProductCreateCommand>
    {
        public async Task Consume(ConsumeContext<ProductCreateCommand> context)
        {
            var message = context.Message;
            await Task.Run(() => Console.WriteLine(message.ProductId));
        }
    }
}
