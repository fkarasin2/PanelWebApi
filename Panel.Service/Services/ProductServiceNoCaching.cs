using AutoMapper;
using Panel.DTOs;
using Panel.Repository;
using Panel.Services;
using Panel.UnitOfWork;

namespace Panel.Service.Services;

public class ProductService : Services<Product>, IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private IProductService _productServiceImplementation;

    public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repository, unitOfWork)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductByCategory()
    {
        var products = await _productRepository.GetProductByCategory();
        var productDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
        return CustomResponseDto<List<ProductWithCategoryDto>>.success(200, productDto);
    }

    public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductCategory(Guid categoryId)
    {
        var products = await _productRepository.GetProductCategoryId(categoryId);
        var productDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
        return CustomResponseDto<List<ProductWithCategoryDto>>.success(200, productDto);
    }
}