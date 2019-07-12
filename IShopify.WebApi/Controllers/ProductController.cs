using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IShopify.Core.Products;
using IShopify.Core.Products.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IShopify.WebApi.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<Product> Get(int id)
        {
            return await _productService.Get(id);
        }
    }
}