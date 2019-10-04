using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IShopify.WebApi.Bootstrap
{
    /// <summary>
    /// Configures MVC
    /// </summary>
    public static class MvcConfig
    {
        /// <summary>
        ///  Registers MVC
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureMvc(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                });

            // TODO configure Cors
            //services.AddCors(x =>
            //{
            //    x.AddPolicy(corsPolicy, policyBuilder => BuildCorsPolicy(corsPolicy, policyBuilder, appSettings));
            //});
        }


    //    private static void BuildCorsPolicy(string policyName, CorsPolicyBuilder builder, IAppSettings appSettings)
    //    {
    //        const string CORS_ALL = "Cors-AllowAll";
    //        const string CORS_ALL_METHOD = "Cors-AllowAllMethods";

    //        switch (policyName)
    //        {
    //            case CORS_ALL: // TODO: policy name as constants
    //                builder.WithOrigins(appSettings.SecurityConfiguration.AllowedOrigins);
    //                builder.AllowAnyHeader();
    //                builder.AllowAnyMethod();
    //                builder.AllowCredentials();
    //                break;
    //            case CORS_ALL_METHOD:
    //                builder.AllowAnyMethod();
    //                break;
    //            default:
    //                throw new InvalidOperationException();
    //        }
    //    }
    //}
    }
}
