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
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        private IWebHostEnvironment Environment;

        private Core.Framework.Logging.ILogger _logger;

        /// <summary>
        /// Constructor for the startup class
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Configures the services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
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
                options.UseNpgsql(AppSettingsProvider.Current.IshopifyDB, b => b.MigrationsAssembly("IShopify.WebApi"));
            });
            _logger.Info("Configured DbContext");

            services.ConfigureSwagger();
            _logger.Info("Configured Swagger");


            services.RegisterAutoMapper();
            _logger.Info("Registered AutoMapper");


            var serviceProvider = services.AddDependencies(Configuration);
            _logger.Info("Configured AllServices");

            return serviceProvider;
        }

        /// <summary>
        /// Configures HTTP pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="serviceProvider"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            app.AddGlobalLogger(serviceProvider);
            _logger.Info("Configured Global Logger");

            app.UseRouting();
            _logger.Info("Configured Routing");

            app.UseHsts();
            _logger.Info("Configured Hsts");

            app.UseSwaggerConfiguration();
            _logger.Info("Configured Swagger UI");

            app.UseHttpsRedirection();
            _logger.Info("Configured HttpsRedirection");

            app.UseAuthentication();
            _logger.Info("Configured Authentication");

            app.UseAuthorization();
            _logger.Info("Configured Authorization");

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers().RequireAuthorization();
            });
            _logger.Info("Configured Endpoints");


            var dbInitizer = serviceProvider.GetRequiredService<DatabaseInitializer>();
            dbInitizer
                .initialize();
                //.SeedAsync().ConfigureAwait(false);
            _logger.Info("Seeded Database");

        }

        private void SetupAppConfiguration(IWebHostEnvironment env, IConfiguration configuration, IServiceCollection services)
        {
                var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
                configuration = builder.Build();
                
                var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

                var envFile = Environment.EnvironmentName == "Development" ? ".env" : "test.env";
                services.AddEnv(x =>
                {
                    x.AddEncoding(Encoding.Default)
                    .AddEnvFile(Path.GetFullPath(envFile))
                    .AddThrowOnError(false);
                });
        }
    }
}
