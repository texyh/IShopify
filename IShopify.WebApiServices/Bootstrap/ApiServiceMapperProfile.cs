using AutoMapper;
using IShopify.Core.Products.Models;
using IShopify.WebApiServices.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.WebApiServices.Bootstrap
{
    public class ApiServiceMapperProfile : Profile
    {
        public ApiServiceMapperProfile()
        {
            CreateMap<Product, ProductSummaryViewModel>();
        }

    }
}
