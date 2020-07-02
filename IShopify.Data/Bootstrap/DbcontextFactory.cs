using System;
using System.Collections.Generic;
using System.Text;
using IShopify.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IShopify.Data.Bootstrap
{
    public static class DbContextFactory
    {
        public static IShopifyDbContext CreateDbcontext()
        {
            var builder = new DbContextOptionsBuilder<IShopifyDbContext>();

            builder.UseNpgsql(AppSettingsProvider.Current.IshopifyDB, x => x.MigrationsAssembly("IShopify.WebApi"));

            return new IShopifyDbContext(builder.Options);
        }
    }
}
