using IShopify.Core;
using IShopify.Core.Customer;
using IShopify.Core.Customer.Models;
using IShopify.Core.Products.Models;
using IShopify.Core.Categories.Models;
using Microsoft.EntityFrameworkCore;
using IShopify.Core.Departments;
using IShopify.Core.Attributes.Models;

namespace IShopify.Data
{
    public class IShopifyDbContext : DbContext
    {
        public IShopifyDbContext(DbContextOptions<IShopifyDbContext> options) : base(options)
        {

        }

        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<ProductCategoryEntity> ProductCategories { get; set; }

        public DbSet<CategoryEntity> Categories { get; set; }

        public DbSet<DepartmentEntity> Departments { get; set; }

        public DbSet<ReviewEntity> Reviews { get; set; }

        public DbSet<AttributeEntity> Attributes { get; set; }

        public DbSet<AttributeValueEntity> AttributeValues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductEntity>().ToTable("Products").Property(x => x.Id).ValueGeneratedOnAdd();

            var productCategoryBuilder = builder.Entity<ProductCategoryEntity>().ToTable("ProductCategories");
            productCategoryBuilder.HasKey(x => new { x.CategoryId, x.ProductId });

            productCategoryBuilder.HasOne(x => x.Category)
                .WithMany(x => x.ProductCategories)
                .HasForeignKey(x => x.CategoryId);

            productCategoryBuilder.HasOne(x => x.Product)
                .WithMany(x => x.ProductCategories)
                .HasForeignKey(x => x.ProductId);

            var categoryBuilder = builder.Entity<CategoryEntity>().ToTable("Categories");
            categoryBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
            categoryBuilder.HasOne(x => x.Department).WithMany(x => x.Categories).HasForeignKey(x => x.DepartmentId);

            var departmentBuilder = builder.Entity<DepartmentEntity>()
                .ToTable("Departments").Property(x => x.Id).ValueGeneratedOnAdd();

            var reviewBuilder = builder.Entity<ReviewEntity>().ToTable("Reviews");
            reviewBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
            reviewBuilder.HasOne(x => x.Customer).WithMany(x => x.Reviews).HasForeignKey(x => x.CustomerId);
            reviewBuilder.HasOne(x => x.Product).WithMany(x => x.Reviews).HasForeignKey(x => x.ProductId);

            var customerBuilder = builder.Entity<CustomerEntity>().ToTable("Customers");
            customerBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
            customerBuilder.HasMany(x => x.Reviews).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId);

            var attributeBuilder = builder.Entity<AttributeEntity>().ToTable("Attributes");
            attributeBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
            attributeBuilder.HasMany(x => x.Values).WithOne(x => x.AttributeEntity).HasForeignKey(x => x.AttributeId);


            var attrivaluesBuilder = builder.Entity<AttributeValueEntity>().ToTable("AttributeValues");
            attrivaluesBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
            attrivaluesBuilder.HasOne(x => x.AttributeEntity).WithMany(x => x.Values).HasForeignKey(x => x.AttributeId);

        }
    }
}

