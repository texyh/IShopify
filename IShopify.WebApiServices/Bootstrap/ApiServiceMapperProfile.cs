using AutoMapper;
using IShopify.Core.Orders.Models;
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

            CreateMap<OrderItem, OrderItemSummaryViewModel>()
                .ForMember(x => x.ProductName, y => y.MapFrom(z => z.Product.Name))
                .ForMember(x => x.imageUrl, y => y.MapFrom(z => z.Product.Image));
        }

    }
}
