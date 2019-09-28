﻿using IShopify.Core.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IShopify.WebApi.Middleware;

namespace IShopify.WebApi.Bootstrap
{
    public static class GlobalLogger
    {
        public static void AddGlobalLogger(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<Framework.ILogger>();
            var appSettings = serviceProvider.GetRequiredService<IOptions<AppSettings>>();

            app.UseExceptionHandler(builder => builder.HandleExceptions(logger, appSettings.Value));
        }
    }
}
