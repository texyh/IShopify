using IShopify.Core;
using IShopify.Core.Common.Models;
using IShopify.Core.Data;
using IShopify.Core.Helpers;
using IShopify.Core.Products.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Data.Repositories
{
    internal class ProductRepository : DataRepository<ProductEntity>, IProductRepository
    {
        private readonly IShopifyDbContext _dbContext;
        public ProductRepository(IShopifyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<ProductEntity>> GetProductInCategory(int categoryId, PagedQuery query)
        {
            ArgumentGuard.NotNull(query, nameof(query));

            return await _dbContext.ProductCategories
                .Where(x => x.CategoryId == categoryId)
                .Skip(query.PageSize * (query.PageNumber -1))
                .Take(query.PageSize)
                .Include(x => x.Product)
                .Select(x => x.Product)
                .ToListAsync();
        }

        public async Task<IList<ProductEntity>> GetProductInDepartment(int departmentId, PagedQuery query)
        {
            ArgumentGuard.NotNull(query, nameof(query));
            ArgumentGuard.NotDefault(departmentId, nameof(departmentId));

            var categoryIds = _dbContext.Categories
                .Where(x => x.DepartmentId == departmentId)
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .Select(x => x.Id)
                .ToList();

            return await _dbContext.ProductCategories
                .Where(x => categoryIds.Contains(x.CategoryId))
                .Include(x => x.Product)
                .Select(x => x.Product)
                .ToListAsync();
                
        }

        public async Task<CategoryEntity> GetProductLocation(int id)
        {
            ArgumentGuard.NotDefault(id, nameof(id));

            return await _dbContext.ProductCategories
                .Where(x => x.ProductId == id)
                .Include(x => x.Category)
                .Select(x => x.Category)
                .Include(x => x.Department)
                .FirstAsync();
        }

        public async Task<IList<ReviewEntity>> GetProductReviews(int id)
        {
            ArgumentGuard.NotDefault(id, nameof(id));

            var reviews = default(IList<ReviewEntity>); // TODO fix, this returns empty reviews
            try
            {
                reviews = await _dbContext.Reviews
                                .Include(x => x.Customer)
                                .Where(x => x.ProductId == id)
                                .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return reviews;
        }

        public Task ReviewProduct(ReviewEntity review)
        {
            ArgumentGuard.NotNull(review, nameof(review)); // TODO move into dataRepository

            _dbContext.Add(review);

            return _dbContext.SaveChangesAsync();
        }

        public async Task<IList<ProductEntity>> Search(ProductQueryModel searchQuery)
        {
            ArgumentGuard.NotNull(searchQuery, nameof(searchQuery));

            var query = _dbContext.Products.AsQueryable()
                .Skip(searchQuery.PageSize* (searchQuery.PageNumber - 1))
                .Take(searchQuery.PageSize);
                

            if(!searchQuery.SearchText.IsNullOrEmpty())
            {
                query = query.Where(
                    x => x.Name.Contains(searchQuery.SearchText) || 
                    x.Description.Contains(searchQuery.SearchText));
            }

            return await query.ToListAsync();
        }
    }
}
