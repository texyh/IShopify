using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IShopify.Core;
using IShopify.Core.Common.Models;
using IShopify.Core.MessageBus;
using IShopify.Core.Products;
using IShopify.Core.Products.Messages;
using IShopify.Core.Products.Models;
using IShopify.Core.Security;
using IShopify.DomainServices.Validation;
using IShopify.WebApiServices;
using IShopify.WebApiServices.ViewModels;
using IShopify.WebApiServices.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IShopify.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing Product
    /// </summary>
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductComposerService _productComposerService;
        private readonly IMessageBus _bus;
        private readonly IUserContext _userContext;

        /// <summary>
        /// Constructor for ProductController
        /// </summary>
        /// <param name="productService"></param>
        /// <param name="productComposerService"></param>
        /// <param name="messageBus"></param>
        /// <param name="userContext"></param>
        public ProductController(
            IProductService productService,
            IProductComposerService productComposerService,
            IMessageBus messageBus,
            IUserContext userContext)
        {
            _productService = productService;
            _productComposerService = productComposerService;
            _bus = messageBus;
            _userContext = userContext;
        }

        /// <summary>
        /// Endpoint for get product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/details")]
        [AllowAnonymous]
        public async Task<Product> Get(int id)
        {
            return await _productService.GetAsync(id);
        }

        /// <summary>
        /// Endpoint for searching products
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<PagedResult<ProductSummaryViewModel>> Search([FromQuery] ProductQueryModel query)
        {
            return await _productComposerService.Search(query);
        }

        /// <summary>
        /// Endpoint for get products of a certain category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("category/{categoryId}")]
        [AllowAnonymous]
        public async Task<PagedResult<ProductSummaryViewModel>> GetProductsInCategory(int categoryId, [FromQuery]PagedQuery query) 
        {
            return await _productComposerService.GetProductInCategory(categoryId, query);
        }

        /// <summary>
        /// Endpoint for getting Products in a department
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("department/{departmentId}")]
        [AllowAnonymous]
        public async Task<PagedResult<ProductSummaryViewModel>> GetProductsInDepartment(int departmentId, [FromQuery]PagedQuery query)
        {
            return await _productComposerService.GetProductInDepartment(departmentId, query);
        }

        /// <summary>
        /// Endpoint for getting product locations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/locations")]
        [AllowAnonymous]
        public async Task<ProductLocationViewModel> GetProductLocations(int id)
        {
            return await _productComposerService.GetProductLocation(id);
        }

        /// <summary>
        /// Endpoint for Product reviews
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/reviews")]
        [AllowAnonymous]
        public async Task<IList<Review>> GetProductReviews(int id)
        {
            return await _productService.GetProductReviewsAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> AddProduct([FromBody] SaveProductModel product)
        {
            var id =  await _productService.AddProductAsync(product);
            await _bus.PublishAsync(new ProductCreateCommand(_userContext.UserId, id));

            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task UpdateProduct(int id, [FromBody]SaveProductModel product)
        {
            await _productService.UpdateProductAsync(id, product);
        }

        /// <summary>
        /// Endpoint for adding a review ot a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="review"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        [HttpPost("{id}/reviews")]
        public async Task ReviewProduct(int id, string review, int rating)
        {
            await _productService.ReviewProductAsync(id, review, rating);
        }
    }
}