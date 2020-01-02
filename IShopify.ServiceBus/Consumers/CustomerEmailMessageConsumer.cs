using System.Threading.Tasks;
using Autofac;
using IShopify.Core.Customer;
using Microsoft.Extensions.Caching.Distributed;
using IShopify.ServiceBus.Helpers;
using MassTransit;
using Microsoft.Extensions.Caching.Memory;
using IShopify.Core.Framework.Logging;

namespace IShopify.ServiceBus.Consumers
{
    public class CustomerEmailMessageConsumer : IConsumer<PasswordResetRequestCommand>
    {
        public async Task Consume(ConsumeContext<PasswordResetRequestCommand> context)
        {
            var message = context.Message;

            await LifetimeScopeRunner.ExecuteAsync(message, async scope =>
            {
                var emailService = scope.Resolve<ICustomerEmailService>();

                await emailService.SendPasswordResetEmailAsync(message.UserId);
            }, catchExceptions: true);
        }
    }
}