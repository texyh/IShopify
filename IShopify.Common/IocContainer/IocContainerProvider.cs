using Autofac;
using IShopify.Core.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Common.IocContainer
{
    public static class IocContainerProvider
    {
        public static IContainer Current { get; private set; }

        public static  void Register(IContainer container)
        {
            Current = container;
        }

        public static void RunInLifetimeScope(Action<ILifetimeScope> operation, Action<ContainerBuilder> configurationAction = null)
        {
            if (configurationAction == null)
            {
                configurationAction = (ContainerBuilder b) => { };
            }

            using (var scope = Current.BeginLifetimeScope(configurationAction))
            {
                operation(scope);
            }
        }

        public static async Task RunInLifetimeScopeAsync(Func<ILifetimeScope, Task> operation, 
            Action<ContainerBuilder> configurationAction = null, bool catchExceptions = false)
        {
            if (configurationAction == null)
            {
                configurationAction = (ContainerBuilder b) => { };
            }

            using (var scope = Current.BeginLifetimeScope(configurationAction))
            {
                try
                {
                    await operation(scope);
                }
                catch (Exception ex)
                {
                    if (!catchExceptions)
                    {
                        throw;
                    }

                    var logger = scope.Resolve<ILogger>();

                    logger.Error(ex);
                }
            }
        }
    }
}
