using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IShopify.DomainServices.Bootstrap;
using IShopify.Framework.Bootstrap;
using IShopify.WebApiServices.Bootstrap;

namespace IShopify.WebApi.Bootstrap
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DomainServicesMapperProfile>();
                cfg.AddProfile<ApiServiceMapperProfile>();
                cfg.AddProfile<FrameworkMapperProfile>();
            });
        }
    }
}
