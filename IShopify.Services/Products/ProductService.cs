using AutoMapper;
using IShopify.Core;
using IShopify.Core.Categories.Models;
using IShopify.Core.Common;
using IShopify.Core.Common.Models;
using IShopify.Core.Data;
using IShopify.Core.Exceptions;
using IShopify.Core.Helpers;
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

        public async Task<int> AddProductAsync(SaveProductModel model)
        {
            ArgumentGuard.NotNull(model, nameof(model));

            var product = _mapper.Map<SaveProductModel, models.Product>(model);

            product.SetPermissions(_permissionFactory.CreateProductPermissions());

            if(!product.Permissions.CanCreate)
            {
                throw new InvalidPermissionException("You do not have permission to create this product");
            }

            await _validatorFactory.ValidateAsync(product);

            var entity = _mapper.Map<models.Product, ProductEntity>(product);

            return await _productRepository.AddAsync(entity);
        }

        public async Task<models.Product> GetAsync(int id)
        {
            var entity =  await _productRepository.GetAsync(id);

            var product =  _mapper.Map<ProductEntity, models.Product>(entity);

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
            var result = await _productRepository.GetProductInCategoryAsync(categoryId, query);

            return ToProduct(result);
        }

        public async Task<IList<models.Product>> GetProductInDepartmentAsync(int departmentId, PagedQuery query)
        {
            query.NormalizePageNumber();
            var result = await _productRepository.GetProductInDepartmentAsync(departmentId, query);

            return ToProduct(result);
        }

        public async Task<Category> GetProductLocationAsync(int id)
        {
            var result = await _productRepository.GetProductLocationAsync(id);

            return _mapper.Map<CategoryEntity, Category>(result);
        }
        public async Task<IList<Review>> GetProductReviewsAsync(int id)
        {
            var result = await _productRepository.GetProductReviewsAsync(id);

            return _mapper.Map<IList<ReviewEntity>, IList<Review>>(result);
        }
        public async Task ReviewProductAsync(int id, string review, int rating)
        {
            var reviewEntity = new ReviewEntity
            {
                CustomerId = _userContext.UserId,
                ProductId = id,
                Rating = rating,
                Review = review,
                CreatedOn = DateTime.UtcNow
            };

            await _productRepository.ReviewProductAsync(reviewEntity);
        }
        public async Task<IList<models.Product>> SearchAsync(ProductQueryModel query)
        {
            query.NormalizePageNumber();
            var result =  await _productRepository.SearchAsync(query);

            return ToProduct(result);
        }

        public async Task UpdateProductAsync(int id, SaveProductModel model)
        {
            var productEntity = await _productRepository.GetAsync(id);

            var product = _mapper.Map<ProductEntity, models.Product>(productEntity);
           
            product.SetPermissions(_permissionFactory.CreateProductPermissions());

            if (!product.Permissions.CanEdit)
            {
                throw new InvalidPermissionException("You do not have the permission to edit this product");
            }

            var updatedFields = ApplyUpdates(product, model);

            if(updatedFields.IsNullOrEmpty())
            {
                return;
            }

            await _validatorFactory.ValidateAsync(product);

            var entity = _mapper.Map<models.Product, ProductEntity>(product);
            
            await _productRepository.UpdateFieldsAsync(entity, updatedFields);
        }

        private IList<models.Product> ToProduct(IList<ProductEntity> products)
        {
            return _mapper.Map<IList<ProductEntity>, IList<models.Product>>(products);
        }

        private string[] ApplyUpdates(models.Product product, SaveProductModel updatedProduct) 
        {
            var updatedFields = new List<string>();

            if(updatedProduct.Description.IsNotNull() && updatedProduct.Description != product.Description)
            {
                updatedFields.Add(nameof(product.Description));
                product.Description = updatedProduct.Description;
            }

            if(!updatedProduct.DisCountedPrice.IsDefault() && updatedProduct.DisCountedPrice != product.DisCountedPrice)
            {
                updatedFields.Add(nameof(product.DisCountedPrice));
                product.DisCountedPrice = updatedProduct.DisCountedPrice;
            }

            if (!updatedProduct.Display.IsDefault() && updatedProduct.Display != product.Display)
            {
                updatedFields.Add(nameof(product.Display));
                product.Display = updatedProduct.Display;
            }

            if (!updatedProduct.Image.IsNullOrEmpty() && updatedProduct.Image != product.Image)
            {
                updatedFields.Add(nameof(product.Image));
                product.Image = updatedProduct.Image;
            }

            if (!updatedProduct.Image2.IsNullOrEmpty() && updatedProduct.Image2 != product.Image2)
            {
                updatedFields.Add(nameof(product.Image2));
                product.Image2 = updatedProduct.Image2;
            }

            if (updatedProduct.Name.IsNotNull() && updatedProduct.Name != product.Name)
            {
                updatedFields.Add(nameof(product.Name));
                product.Name = updatedProduct.Name;
            }

            if (!updatedProduct.Price.IsDefault() && updatedProduct.Price != product.Price)
            {
                updatedFields.Add(nameof(product.Price));
                product.Price = updatedProduct.Price;
            }

            return updatedFields.ToArray();
        }
    }
}
