using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IShopify.Core;
using IShopify.Core.Common.Models;
using IShopify.Core.Products;
using IShopify.Core.Products.Models;
using IShopify.WebApiServices;
using IShopify.WebApiServices.ViewModels;
using IShopify.WebApiServices.ViewModels.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IShopify.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductComposerService _productComposerService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productService"></param>
        /// <param name="productComposerService"></param>
        public ProductController(
            IProductService productService,
            IProductComposerService productComposerService)
        {
            _productService = productService;
            _productComposerService = productComposerService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/details")]
        public async Task<Product> Get(int id)
        {
            return await _productService.Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("search")]
        public async Task<PagedResult<ProductSummaryViewModel>> Search([FromQuery] ProductQueryModel query)
        {
            return await _productComposerService.Search(query);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<PagedResult<ProductSummaryViewModel>> GetProductsInCategory(int categoryId, [FromQuery]PagedQuery query) 
        {
            return await _productComposerService.GetProductInCategory(categoryId, query);
        }

        [HttpGet("department/{departmentId}")]
        public async Task<PagedResult<ProductSummaryViewModel>> GetProductsInDepartment(int departmentId, [FromQuery]PagedQuery query)
        {
            return await _productComposerService.GetProductInDepartment(departmentId, query);
        }

        [HttpGet("{id}/locations")]
        public async Task<ProductLocationViewModel> GetProductLocations(int id)
        {
            return await _productComposerService.GetProductLocation(id);
        }

        [HttpGet("{id}/reviews")]
        public async Task<IList<Review>> GetProductReviews(int id)
        {
            return await _productService.GetProductReviewsAsync(id);
        }

        [HttpPost("{id}/reviews")]
        public async Task ReviewProduct(int id, string review, int rating)
        {
            await _productService.ReviewProduct(id, review, rating);
        }




    }
}