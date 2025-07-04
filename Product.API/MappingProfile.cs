using AutoMapper;
using Infrastructure.Mappings;
using Product.API.Entities;
using Shared.DTOs.Product;

namespace Product.API
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CardProduct, ProductDto>();
            //CreateMap<CreateProductDto, CardProduct>();
            //CreateMap<UpdateProductDto, CardProduct>().IgnoreAllNonExisting();
        }
    }
}
