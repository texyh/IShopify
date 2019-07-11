using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IShopify.WebApi.Bootstrap
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new Info
                {
                    Title = "IShopify",
                    Version = "v1",
                    Description = "A system for buying and sellings goods",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Emeka LLC",
                        Email = string.Empty,
                        Url = string.Empty
                    }
                });

                var basePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "IShopify.WebApi.xml");

                opt.IncludeXmlComments(string.Format(basePath));
            });

            
        }
    }
}
