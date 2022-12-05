using AutoMapper;
using Panel.DTOs;
using Panel.Repository;
using Panel.Services;
using Panel.UnitOfWork;

namespace Panel.Service.Services;

public class CategoryService : Services<Category>, ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;
    
    public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository) : base(repository, unitOfWork)
    {
        _mapper = mapper;
        _repository = categoryRepository;
    }

    public async Task<CustomResponseDto<CategoryWithProducts>> GetCategoryWithProductsByIdAsync(Guid id)
    {
        var category = await _repository.GetCategoryWithProductsByIdAsync(id);
        var categoryDto = _mapper.Map<CategoryWithProducts>(category);
        return CustomResponseDto<CategoryWithProducts>.success(200, categoryDto);
    }
}
