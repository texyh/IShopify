using IShopify.Core.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IShopify.WebApi.Middleware;
using IShopify.Common;
using IShopify.Core.Framework.Logging;

namespace IShopify.WebApi.Bootstrap
{
    /// <summary>
    /// Configures the global logger
    /// </summary>
    public static class GlobalLogger
    {
        /// <summary>
        /// Configures the global logger handler
        /// </summary>
        /// <param name="app"></param>
        /// <param name="serviceProvider"></param>
        public static void AddGlobalLogger(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<ILogger>();

            app.UseExceptionHandler(builder => builder.HandleExceptions(logger, AppSettingsProvider.Current));
        }
    }
}
