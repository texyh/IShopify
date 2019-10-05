using AutoMapper;
using IShopify.Core;
using IShopify.Core.Common;
using IShopify.Core.Common.Models;
using IShopify.Core.Data;
using IShopify.Core.Exceptions;
using IShopify.Core.Products;
using IShopify.Core.Products.Models;
using IShopify.Core.Security;
using IShopify.DomainServices.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using models = IShopify.Core.Products.Models;

namespace IShopify.DomainServices.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        private readonly IValidatorFactory _validatorFactory;

        private readonly IPermissionFactory _permissionFactory;

        public ProductService(
            IProductRepository productRepository, 
            IUserContext userContext,
            IValidatorFactory validatorFactory,
            IPermissionFactory permissionFactory,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _userContext = userContext;
            _validatorFactory = validatorFactory;
            _permissionFactory = permissionFactory;
            _mapper = mapper;
        }
        public async Task<models.Product> Get(int id)
        {
            var entity =  await _productRepository.GetAsync(id, true);

            var product =  Mapper.Map<ProductEntity, models.Product>(entity);

            product.SetPermissions(_permissionFactory.CreateProductPermissions());

            if(!product.Permissions.CanView)
            {
                throw new InvalidPermissionException("You do not have access to view this product");
            }

            return product;

        }

        public async Task<IList<models.Product>> GetProductInCategoryAsync(int categoryId, PagedQuery query)
        {
            query.NormalizePageNumber();
            var result = await _productRepository.GetProductInCategory(categoryId, query);

            return ToProduct(result);
        }

        public async Task<IList<models.Product>> GetProductInDepartmentAsync(int departmentId, PagedQuery query)
        {
            query.NormalizePageNumber();
            var result = await _productRepository.GetProductInDepartment(departmentId, query);
            return ToProduct(result);
        }

        public async Task<Category> GetProductLocationAsync(int id)
        {
            var result = await _productRepository.GetProductLocation(id);
            return _mapper.Map<CategoryEntity, Category>(result);
        }

        public async Task<IList<Review>> GetProductReviewsAsync(int id)
        {
            var result = await _productRepository.GetProductReviews(id);
            return _mapper.Map<IList<ReviewEntity>, IList<Review>>(result);
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

        public async Task<IList<models.Product>> SearchAsync(ProductQueryModel query)
        {
            query.NormalizePageNumber();
            var result =  await _productRepository.Search(query);

            return ToProduct(result);
        }

        private IList<models.Product> ToProduct(IList<ProductEntity> products)
        {
            return Mapper.Map<IList<ProductEntity>, IList<models.Product>>(products);
        }
    }
}
