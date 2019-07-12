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
            var productBuilder = builder.Entity<ProductEntity>().ToTable("product");
            productBuilder.Property(x => x.Id).HasColumnName("product_id");
            productBuilder.Property(x => x.DisCountedPrice).HasColumnName("discounted_price");
            productBuilder.Property(x => x.Image2).HasColumnName("image_2");
        }
    }
}

