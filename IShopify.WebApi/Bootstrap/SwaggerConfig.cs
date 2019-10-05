using IShopify.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IShopify.WebApi.Bootstrap
{
    /// <summary>
    /// Configures Swagger
    /// </summary>
    public static class SwaggerConfig
    {
        private const string SwaggerOpenAPISpecification = "/swagger/v1/swagger.json";
        private const string SwaggerOpenAPISpecificationDisplayName = "IShopify Api";

        /// <summary>
        /// Registers Swagger.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "IShopify",
                    Version = "2.0",
                    Description = "A system for buying and sellings goods",
                    TermsOfService = new Uri("http://example.com"),
                    Contact = new OpenApiContact
                    {
                        Name = "Emeka LLC",
                        Email = string.Empty,
                        Url = new Uri("http://example.com")
                    }
                });

                var basePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "IShopify.WebApi.xml");

                opt.IncludeXmlComments(string.Format(basePath));
            });

            services.ConfigureSwaggerGen(opt =>
            {
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference =  new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                        },
                        new string[] {}
                    }
                });

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Flows =  new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("/login", UriKind.Relative)
                        }
                    }
                });

            });


        }

        /// <summary>
        /// Configures swagger ui
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerConfiguration (this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint(SwaggerOpenAPISpecification, SwaggerOpenAPISpecificationDisplayName);
            });
        }
    }
}
