using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using dotenv.net.DependencyInjection.Extensions;
using IShopify.Common;
using IShopify.Core.Config;
using IShopify.Data;
using IShopify.DomainServices.Bootstrap;
using IShopify.Framework.Bootstrap;
using IShopify.WebApi.Bootstrap;
using IShopify.WebApiServices.Bootstrap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IShopify.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private IHostingEnvironment Environment;

        private Core.Framework.Logging.ILogger _logger;


        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            SetupAppConfiguration(Environment, Configuration, services);
            AppSettingsProvider.Register(new AppSettings(Configuration));

            _logger = SystemLogFactory.Create();

            services.ConfigureMvc();
            _logger.Info("Configured MVC");

            services.AddApplicationAuthentication();
            _logger.Info("Configured App Auth");

            services.AddDbContextPool<IShopifyDbContext>(options =>
            {
                options.UseMySql(AppSettingsProvider.Current.IshopifyDB);
            });
            _logger.Info("Configured DbContext");

            services.ConfigureSwagger();
            _logger.Info("Configured Swagger");


            AutoMapperConfig.Initialize();
            _logger.Info("Initialized AutoMapper");


            var serviceProvider = services.AddDependencies(Configuration);
            _logger.Info("Configured AllServices");

            return serviceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            app.AddGlobalLogger(serviceProvider);
            _logger.Info("Configured Global Logger");

            app.UseHsts();
            _logger.Info("Configured Hsts");

            app.UseSwaggerConfiguration();
            _logger.Info("Configured Swagger UI");

            app.UseHttpsRedirection();
            _logger.Info("Configured HttpsRedirection");

            app.UseMvc();
            _logger.Info("Configured Mvc");

            app.UseAuthentication();
            _logger.Info("Configured Authentication");

        }

        private void SetupAppConfiguration(IHostingEnvironment env, IConfiguration configuration, IServiceCollection services)
        {
                var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
                configuration = builder.Build();
                
                var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

                var envFile = Environment.IsDevelopment() ? ".env" : "test.env";
                services.AddEnv(x =>
                {
                    x.AddEncoding(Encoding.Default)
                    .AddEnvFile(Path.GetFullPath(envFile))
                    .AddThrowOnError(false);
                });
        }
    }
}
