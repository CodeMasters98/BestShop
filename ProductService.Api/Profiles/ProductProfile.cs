using AutoMapper;
using ProductService.Api.Dtos.Product;
using ProductService.Api.Entities;

namespace ProductService.Api.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<AddProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        CreateMap<Product, ProductDto>();
    }
}
