using Panel.DTOs;

namespace Panel.Services;

public interface IProductService : IService<Product>
{
    Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductByCategory();

    Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductCategory(Guid categoryId);
}