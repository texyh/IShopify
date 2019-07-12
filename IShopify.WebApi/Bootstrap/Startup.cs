using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IShopify.Data;
using IShopify.DomainServices.Bootstrap;
using IShopify.WebApi.Bootstrap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IShopify.WebApi
{
    public class Startup
    {
        private const string SwaggerOpenAPISpecification = "/swagger/v1/swagger.json";
        private const string SwaggerOpenAPISpecificationDisplayName = "IShopify Api";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContextPool<IShopifyDbContext>(options =>
            {
                options.UseMySql(Configuration["ConnectionString"]);
            });

            services.ConfigureSwagger();

            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<DomainServicesMapperProfile>());

            var serviceProvider = services.AddDependencies();
            return serviceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint(SwaggerOpenAPISpecification, SwaggerOpenAPISpecificationDisplayName);
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
