using IShopify.Core.Products.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Data
{
    public class IShopifyDbContext : DbContext
    {
        public IShopifyDbContext(DbContextOptions<IShopifyDbContext> options) : base(options)
        {
            
        }

        public DbSet<ProductEntity> Products { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

