using IShopify.Core.Common;
using IShopify.Core.Customer.Models;
using IShopify.Core.Helpers;
using IShopify.Core.Products.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Data
{
    public class DatabaseInitializer
    {
        private readonly IShopifyDbContext _dbContext;

        private const string ProductFile = "Product";

        private const string CategoryFile = "Category";

        private const string DepartmenttFile = "Department";

        private readonly Assembly excutionAssembly = Assembly.GetExecutingAssembly();

        private readonly ITemplateLoader _templateLoader;

        public DatabaseInitializer(
            IShopifyDbContext dbContext,
            ITemplateLoader templateLoader)
        {
            _dbContext = dbContext;
            _templateLoader = templateLoader;
        }


        public void initialize()
        {
            _dbContext.Database.Migrate();

            //return this;
        }

        public async Task SeedAsync()
        {
            await AddProductAsync();
            await AddDeparmentAsync();
            await AddCategoryAsync();
            await AddCustomersAsync();
        }
    
        private async Task AddCustomersAsync() 
        {
            if(!await _dbContext.Customers.AnyAsync()) 
            {
                var customer = new CustomerEntity 
                {
                    Name = "Emeka",
                    Email = "emeka@example.com",
                    Password = "86e814aaba9f81d764c63e2e02233b55234dacc862637bafd6c549928770c448"
                };

                 _dbContext.Customers.Add(customer);

                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task AddProductAsync()
        {
            if(!await _dbContext.Products.AnyAsync())
            {
                var productString = await GetFileContentAsync(ProductFile);
                var products = productString.FromJson<IList<ProductEntity>>();
                products.ForEach(x => x.Id = 0);

                _dbContext.Products.AddRange(products);

                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task AddCategoryAsync()
        {
            if(!await _dbContext.Categories.AnyAsync())
            {
                var categoryString = await GetFileContentAsync(CategoryFile);
                var categories = categoryString.FromJson<IList<CategoryEntity>>();
                categories.ForEach(x => x.Id =0);

                 _dbContext.Categories.AddRange(categories);

                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task AddDeparmentAsync()
        {
            if(!await _dbContext.Departments.AnyAsync())
            {
                var departmentString = await GetFileContentAsync(DepartmenttFile);
                var departments = departmentString.FromJson<IList<DepartmentEntity>>();
                departments.ForEach(x => x.Id = 0);

                _dbContext.Departments.AddRange(departments);

                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task<string> GetFileContentAsync(string templateName)
        {
            return await _templateLoader.LoadTemplateAsStringAsync(excutionAssembly, templateName);
        }
    }
}
