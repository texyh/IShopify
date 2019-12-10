using IShopify.Core;
using IShopify.Core.Customer;
using IShopify.Core.Customer.Models;
using IShopify.Core.Products.Models;
using IShopify.Core.Categories.Models;
using Microsoft.EntityFrameworkCore;
using IShopify.Core.Departments;
using IShopify.Core.Attributes.Models;
using IShopify.Core.Orders.Models.Entity;
using IShopify.Core.Orders.Models.Entities;
using IShopify.Core.Security;

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

        public DbSet<ProductAttributeValueEntity> ProductAttributes { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<OrderItemEntity> OrderItems { get; set; }

        public DbSet<AddressEntity> Addresses { get; set; }

        public DbSet<CustomerEntity> Customers { get; set; }

        public DbSet<AccessKeyEntity> AccessKeys {get; set;}

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

            builder.Entity<ProductAttributeValueEntity>()
                .HasKey(x => new { x.AttributeValueId, x.ProductId });

            builder.Entity<ProductAttributeValueEntity>()
                .HasOne(x => x.Product)
                .WithMany(x => x.ProductAttributeValues)
                .HasForeignKey(x => x.ProductId);

            builder.Entity<ProductAttributeValueEntity>()
                .HasOne(x => x.AttributeValue)
                .WithMany(x => x.ProductAttributeValues)
                .HasForeignKey(x => x.AttributeValueId);

            var categoryBuilder = builder.Entity<CategoryEntity>().ToTable("Categories");
            categoryBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
            categoryBuilder.HasOne(x => x.Department)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.DepartmentId);

            var departmentBuilder = builder.Entity<DepartmentEntity>()
                .ToTable("Departments").Property(x => x.Id).ValueGeneratedOnAdd();

            var reviewBuilder = builder.Entity<ReviewEntity>().ToTable("Reviews");
            reviewBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
            reviewBuilder.HasOne(x => x.Customer)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.CustomerId);
            reviewBuilder.HasOne(x => x.Product)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.ProductId);

            var customerBuilder = builder.Entity<CustomerEntity>().ToTable("Customers");
            customerBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
            customerBuilder.HasMany(x => x.Reviews)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);
            customerBuilder.HasMany(x => x.Orders)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);
            customerBuilder.HasMany(x => x.Addresses)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);
            customerBuilder.Ignore(x => x.AuthProfile);
            customerBuilder.Property(x => x.AuthProfileJson).HasColumnName("AuthProfile");

        

            var attributeBuilder = builder.Entity<AttributeEntity>().ToTable("Attributes");
            attributeBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
            attributeBuilder.HasMany(x => x.Values)
                .WithOne(x => x.Attribute)
                .HasForeignKey(x => x.AttributeId);

            var attrivaluesBuilder = builder.Entity<AttributeValueEntity>().ToTable("AttributeValues");
            attrivaluesBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
            attrivaluesBuilder.HasOne(x => x.Attribute)
                .WithMany(x => x.Values)
                .HasForeignKey(x => x.AttributeId);

            var orderBuilder = builder.Entity<OrderEntity>();
            orderBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
            orderBuilder.HasOne(x => x.ShippigAddress).WithMany(x => x.Orders)
                .HasForeignKey(x => x.ShippingAddressId).IsRequired(false);
            orderBuilder.HasMany(x => x.OrderItems).WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);
            orderBuilder.HasOne(x => x.Customer).WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId);
            orderBuilder.HasOne(x => x.BillingAddress).WithMany()
                .HasForeignKey(x => x.BillingAddressId).IsRequired(false);

            var orderItemBuilder = builder.Entity<OrderItemEntity>();
            orderItemBuilder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);
            orderItemBuilder.HasOne(x => x.Order)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.OrderId);

            var shoppingAddress = builder.Entity<AddressEntity>();
            shoppingAddress.Property(x => x.Id).ValueGeneratedOnAdd();
            shoppingAddress.HasOne(x => x.Customer)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.CustomerId);
            shoppingAddress.HasMany(x => x.Orders)
                .WithOne(x => x.ShippigAddress)
                .HasForeignKey(x => x.ShippingAddressId);

            var accessBuilder = builder.Entity<AccessKeyEntity>();
            accessBuilder.Ignore(x => x.Scopes);
            accessBuilder.Property(x => x.ScopesJson).HasColumnName("Scopes");
            accessBuilder.HasOne(x => x.Customer).WithMany(x => x.AccessKeys)
                .HasForeignKey(x => x.UserId);
        }
    }
}

