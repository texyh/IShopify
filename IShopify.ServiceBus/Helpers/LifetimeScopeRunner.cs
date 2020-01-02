using System;
using Autofac;
using IShopify.Core.MessageBus;
using IShopify.Common.IocContainer;
using IShopify.Core.Security;
using System.Threading.Tasks;
using IShopify.Common;
using IShopify.Core.Customer;

namespace IShopify.ServiceBus.Helpers
{
    public static class LifetimeScopeRunner
    {
        public static void Execute(IMessage message, Action<ILifetimeScope> operation)
        {
            IocContainerProvider.RunInLifetimeScope(operation, configurationAction: 
                builder =>
                {
                    builder.Register(x => CreateUserContext(x, message.UserId))
                    .As<IUserContext>();
                });
        }

        public static async Task ExecuteAsync(IMessage message, Func<ILifetimeScope, Task> operation, bool catchExceptions = false)
        {
            await IocContainerProvider.RunInLifetimeScopeAsync(operation, configurationAction:
                builder =>
                {
                    builder.Register(x => CreateUserContext(x, message.UserId))
                    .As<IUserContext>();
                }, catchExceptions: catchExceptions);
        }

        public static async Task ExecuteAsync(int userId, Func<ILifetimeScope, Task> operation, bool catchExceptions = false)
        {
            await IocContainerProvider.RunInLifetimeScopeAsync(operation, configurationAction:
                builder =>
                {
                    builder.Register(x => CreateUserContext(x, userId))
                    .As<IUserContext>();
                }, catchExceptions: catchExceptions);
        }

        private static UserContext CreateUserContext(IComponentContext componentContext, int userId)
        {
            return new UserContext(
                    userId,
                    componentContext.Resolve<ICustomerLookupService>());
        }
    }
}