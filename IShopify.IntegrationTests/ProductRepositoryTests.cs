using Autofac;
using AutoMapper;
using IShopify.Core.Data;
using IShopify.Core.Products.Models;
using IShopify.Data;
using IShopify.IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace IShopify.IntegrationTests
{
    [Collection("IntegrationTests")]
    public class ProductRepositoryTests
    {
        private IContainer _container;

        public ProductRepositoryTests()
        {
            _container = IocConfig.Register();
            var db = _container.Resolve<DatabaseInitializer>();
            db.initialize();
        }

        [Fact]
        public void CanUpdateFields()
        {
            using (var scope = _container.BeginLifetimeScope())
            using (var context = scope.Resolve<IShopifyDbContext>())
            using (var repo = scope.Resolve<IProductRepository>())
            using (var txn  = context.Database.BeginTransaction())
            {
                var product = Products[0];

                var id = repo.AddAsync(product).GetAwaiter().GetResult();

                var updatedProduct = new ProductEntity
                {
                    Id = id,
                    Name = "TestingUpdated",
                    Description = product.Description,
                    Price = 30
                };

                repo.UpdateFieldsAsync(updatedProduct, nameof(updatedProduct.Name), nameof(updatedProduct.Price))
                    .GetAwaiter().GetResult();

                var dbProduct = context.Set<ProductEntity>().Find(id);

                txn.Rollback();

                Assert.NotNull(dbProduct);
                Assert.Equal(updatedProduct.Name, dbProduct.Name);
                Assert.Equal(updatedProduct.Price, dbProduct.Price);
            }
        }


        public IList<ProductEntity> Products
        {
            get
            {
                return new List<ProductEntity>
                {
                    new ProductEntity
                    {
                        Name = "TestProduct1",
                        Description = "Product Test1"
                    },

                    new ProductEntity
                    {
                        Name = "TestProduct2",
                        Description = "Product Test2"
                    }
                };
            }
        }

        
    }
}
