using IShopify.Core.Framework.Logging;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IShopify.WebApi.Bootstrap
{
    /// <summary>
    /// class for starting service bus
    /// </summary>
    public static class ServiceBusConfig
    {
        /// <summary>
        /// class for starting service bus
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        public static Task StartServiceBusAsync(this IServiceProvider serviceProvider, ILogger logger)
        {
            var bus = serviceProvider.GetService<IBusControl>();

            try
            {
                return bus.StartAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }

    }
}
