using Panel.DTOs;

namespace Panel.Services;

public interface ICategoryService : IService<Category>
{
    Task<CustomResponseDto<CategoryWithProducts>> GetCategoryWithProductsByIdAsync(Guid id);
}