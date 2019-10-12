using AutoMapper;
using IShopify.Core;
using IShopify.Core.Customer.Models;
using IShopify.Core.Helpers;
using IShopify.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;
using models = IShopify.Core.Customer.Models;
using productModels = IShopify.Core.Products.Models;

namespace IShopify.DomainServices.Bootstrap
{
    public class DomainServicesMapperProfile : Profile
    {
        public DomainServicesMapperProfile()
        {
            CreateMap<ProductEntity, productModels.Product>().ReverseMap();

            CreateMap<CategoryEntity, Category>();

            CreateMap<DepartmentEntity, Department>();

            CreateMap<ReviewEntity, Review>()
                .ForMember(x => x.Name, y => y.MapFrom(x => x.Customer.Name));

            CreateMap<SaveCustomerViewModel, CustomerEntity>()
                .ForAllMembers(x => x.Condition(y => !y.IsNull()));

            CreateMap<SaveCustomerAddressViewModel, CustomerEntity>()
                .ForAllMembers(x => x.Condition(y => !y.IsNull()));

            CreateMap<CustomerEntity, models.Customer>();

            CreateMap<productModels.Product, SaveProductModel>().ReverseMap();
        }
    }
}
