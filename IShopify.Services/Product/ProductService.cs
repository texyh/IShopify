using AutoMapper;
using IShopify.Core;
using IShopify.Core.Common.Models;
using IShopify.Core.Data;
using IShopify.Core.Products;
using IShopify.Core.Products.Models;
using IShopify.Core.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.DomainServices.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserContext _userContext;

        public ProductService(IProductRepository productRepository, IUserContext userContext)
        {
            _productRepository = productRepository;
            _userContext = userContext;
        }
        public async Task<Product> Get(int id)
        {
            var entity =  await _productRepository.GetAsync(id, true);
            return Mapper.Map<ProductEntity, Product>(entity);
        }

        public async Task<IList<Product>> GetProductInCategoryAsync(int categoryId, PagedQuery query)
        {
            query.NormalizePageNumber();
            var result = await _productRepository.GetProductInCategory(categoryId, query);
            return ToProduct(result);
        }

        public async Task<IList<Product>> GetProductInDepartmentAsync(int departmentId, PagedQuery query)
        {
            query.NormalizePageNumber();
            var result = await _productRepository.GetProductInDepartment(departmentId, query);
            return ToProduct(result);
        }

        public async Task<Category> GetProductLocationAsync(int id)
        {
            var result = await _productRepository.GetProductLocation(id);
            return Mapper.Map<CategoryEntity, Category>(result);
        }

        public async Task<IList<Review>> GetProductReviewsAsync(int id)
        {
            var result = await _productRepository.GetProductReviews(id);
            return Mapper.Map<IList<ReviewEntity>, IList<Review>>(result);
        }

        public async Task ReviewProduct(int id, string review, int rating)
        {
            var reviewEntity = new ReviewEntity
            {
                CustomerId = _userContext.UserId,
                ProductId = id,
                Rating = rating,
                Review = review,
                CreatedOn = DateTime.UtcNow
            };

            await _productRepository.ReviewProduct(reviewEntity);
        }

        public async Task<IList<Product>> SearchAsync(ProductQueryModel query)
        {
            query.NormalizePageNumber();
            var result =  await _productRepository.Search(query);

            return ToProduct(result);
        }

        private IList<Product> ToProduct(IList<ProductEntity> products)
        {
            return Mapper.Map<IList<ProductEntity>, IList<Product>>(products);
        }
    }
}
