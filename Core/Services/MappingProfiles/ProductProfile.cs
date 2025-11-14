using AutoMapper;
using Domain.Entities.BasketModule;
using Domain.Entities.ProductModule;
using Shared.Dtos.BasketDtos;
using Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product,ProductResultDto>()
                .ForMember(dest => dest.TypeName, options => options.MapFrom(src=>src.ProductType.Name))
                .ForMember(dest=>dest.BrandName,options=>options.MapFrom(src=>src.ProductBrand.Name))
                .ForMember(dest=>dest.PictureUrl,options=>options.MapFrom<PictureUrlResolver>());
            CreateMap<ProductBrand, BrandResultDto>();
            CreateMap<ProductType,TypeResultDto>();

           
        }
    }
}
