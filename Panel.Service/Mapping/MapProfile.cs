using AutoMapper;
using Panel.DTOs;

namespace Panel.Service.Mapping;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
        CreateMap<ProductUpdateDto, Product>();
        CreateMap<Product, ProductWithCategoryDto>();
        CreateMap<Category, CategoryWithProducts>();
        CreateMap<Product, NewProductWithCategory>();
    }
}