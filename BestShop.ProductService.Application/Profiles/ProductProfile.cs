using AutoMapper;
using BestShop.ProductService.Application.Dtos.Product;
using BestShop.ProductService.Domain.Entities;

namespace BestShop.ProductService.Application.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<AddProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        CreateMap<Product, ProductDto>();
    }
}
