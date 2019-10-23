using AutoMapper;
using IShopify.Core;
using IShopify.Core.Categories.Models;
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

        private readonly IMapper _mapper;
        public ProductRepository(
            IShopifyDbContext dbContext,
            IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IList<ProductEntity>> GetProductInCategoryAsync(int categoryId, PagedQuery query)
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

        public async Task<IList<ProductEntity>> GetProductInDepartmentAsync(int departmentId, PagedQuery query)
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

        public async Task<CategoryEntity> GetProductLocationAsync(int id)
        {
            ArgumentGuard.NotDefault(id, nameof(id));

            return await _dbContext.ProductCategories
                .Where(x => x.ProductId == id)
                .Include(x => x.Category)
                .Select(x => x.Category)
                .Include(x => x.Department)
                .FirstAsync();
        }

        public async Task<IList<ReviewEntity>> GetProductReviewsAsync(int id)
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

        public Task ReviewProductAsync(ReviewEntity review)
        {
            ArgumentGuard.NotNull(review, nameof(review)); // TODO move into dataRepository

            _dbContext.Reviews.Add(review); // TODO add this to the right dbset "Reveiws"

            return _dbContext.SaveChangesAsync();
        }

        public async Task<IList<ProductEntity>> SearchAsync(ProductQueryModel searchQuery)
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

        public void Dispose()
        {
            if(_dbContext.IsNull())
            {
                return;
            }

            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
