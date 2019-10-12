using IShopify.Core.Common;
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
        }

        private async Task AddProductAsync()
        {
            if(!await _dbContext.Products.AnyAsync())
            {
                var productString = await GetFileContentAsync(ProductFile);
                var products = productString.FromJson<IList<ProductEntity>>();

                await _dbContext.Products.AddRangeAsync(products);

                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task AddCategoryAsync()
        {
            if(!await _dbContext.Categories.AnyAsync())
            {
                var categoryString = await GetFileContentAsync(CategoryFile);
                var categories = categoryString.FromJson<IList<CategoryEntity>>();

                await _dbContext.Categories.AddRangeAsync(categories);

                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task AddDeparmentAsync()
        {
            if(!await _dbContext.Departments.AnyAsync())
            {
                var departmentString = await GetFileContentAsync(DepartmenttFile);
                var departments = departmentString.FromJson<IList<DepartmentEntity>>();

                await _dbContext.Departments.AddRangeAsync(departments);

                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task<string> GetFileContentAsync(string templateName)
        {
            return await _templateLoader.LoadTemplateAsStringAsync(excutionAssembly, templateName);
        }
    }
}
