using IShopify.Core;
using IShopify.Core.Customer;
using IShopify.Core.Products.Models;
using Microsoft.EntityFrameworkCore;

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

        public DbSet<DepartmentEntity> DepartMents { get; set; }

        public DbSet<ReviewEntity> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            var productBuilder = builder.Entity<ProductEntity>().ToTable("product");
            productBuilder.Property(x => x.Id).HasColumnName("product_id");
            productBuilder.Property(x => x.DisCountedPrice).HasColumnName("discounted_price");
            productBuilder.Property(x => x.Image2).HasColumnName("image_2");

            var productCategoryBuilder = builder.Entity<ProductCategoryEntity>().ToTable("product_category");
            productCategoryBuilder.Property(x => x.CategoryId).HasColumnName("category_id");
            productCategoryBuilder.Property(x => x.ProductId).HasColumnName("product_id");
            productCategoryBuilder.HasKey(x => new { x.CategoryId, x.ProductId });

            productCategoryBuilder.HasOne(x => x.Category)
                .WithMany(x => x.ProductCategories)
                .HasForeignKey(x => x.CategoryId);

            productCategoryBuilder.HasOne(x => x.Product)
                .WithMany(x => x.ProductCategories)
                .HasForeignKey(x => x.ProductId);

            var categoryBuilder = builder.Entity<CategoryEntity>().ToTable("category");
            categoryBuilder.Property(x => x.Id).HasColumnName("category_id");
            categoryBuilder.Property(x => x.DepartmentId).HasColumnName("department_id");
            categoryBuilder.HasOne(x => x.Department).WithMany(x => x.Categories).HasForeignKey(x => x.DepartmentId);

            var departmentBuilder = builder.Entity<DepartmentEntity>().ToTable("department");
            departmentBuilder.Property(x => x.Id).HasColumnName("department_id");

            var reviewBuilder = builder.Entity<ReviewEntity>().ToTable("review");
            reviewBuilder.Property(x => x.Id).HasColumnName("review_id").ValueGeneratedOnAdd();
            reviewBuilder.Property(x => x.CustomerId).HasColumnName("customer_id");
            reviewBuilder.Property(x => x.ProductId).HasColumnName("product_id");
            reviewBuilder.Property(x => x.CreatedOn).HasColumnName("created_on");
            reviewBuilder.HasOne(x => x.Customer).WithMany(x => x.Reviews).HasForeignKey(x => x.CustomerId);
            reviewBuilder.HasOne(x => x.Product).WithMany(x => x.Reviews).HasForeignKey(x => x.ProductId);

            var customerBuilder = builder.Entity<CustomerEntity>().ToTable("customer");
            customerBuilder.Property(x => x.Id).HasColumnName("customer_id");
            customerBuilder.HasMany(x => x.Reviews).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId);

        }
    }
}

