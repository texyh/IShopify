using AutoMapper;
using IShopify.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.DomainServices.Bootstrap
{
    public class DomainServicesMapperProfile : Profile
    {
        public DomainServicesMapperProfile()
        {
            CreateMap<ProductEntity, Product>();
        }
    }
}
