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
        private IShopifyDbContext _dbcontext;

        public ProductRepositoryTests()
        {
            _container = IocConfig.Register();
            _dbcontext = _container.Resolve<IShopifyDbContext>();
            _dbcontext.Database.Migrate();
        }

        [Fact]
        public void CanUpdateFields()
        {
            using (var scope = _container.BeginLifetimeScope())
            using (var txn  = _dbcontext.Database.BeginTransaction())
            {
                var repo = scope.Resolve<IProductRepository>();
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

                var dbProduct = _dbcontext.Set<ProductEntity>().Find(id);

                txn.Rollback();

                Assert.NotNull(dbProduct);
                Assert.Equal(updatedProduct.Name, dbProduct.Name);
                Assert.Equal(updatedProduct.Price, dbProduct.Price);
            }
        }

        [Fact]
        public void CanDeleteEntities()
        {
            using (var scope = _container.BeginLifetimeScope())
            using (var txn  = _dbcontext.Database.BeginTransaction())
            {
                var repo = scope.Resolve<IProductRepository>();
                var product = Products[0];

                var id = repo.AddAsync(product).GetAwaiter().GetResult();

                _dbcontext.Entry(product).State =  EntityState.Detached;

                repo.DeleteAsync(id)
                    .GetAwaiter().GetResult();

                var dbProduct = _dbcontext.Set<ProductEntity>().Find(id);

                txn.Rollback();

                Assert.Null(dbProduct);
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
