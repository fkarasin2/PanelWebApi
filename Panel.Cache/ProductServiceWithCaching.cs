using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Panel.DTOs;
using Panel.Repository;
using Panel.Service.Exceptions;
using Panel.Services;
using Panel.UnitOfWork;

namespace Panel.Cache;

public class ProductServiceWithCaching : IProductService
{
    private const string CacheProductKey = "productsCache";
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private IProductService _productServiceImplementation;

    public ProductServiceWithCaching( IMapper mapper, IMemoryCache memoryCache, IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _memoryCache = memoryCache;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;

        if (!_memoryCache.TryGetValue(CacheProductKey, out _))
        {
            _memoryCache.Set(CacheProductKey, _productRepository.GetProductByCategory().Result);
        }
    }
    
    public Task<Product> GetByIdAsync(Guid id)
    {
        var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.id == id);
        if (product == null)
        {
            throw new NotFoundException($"{typeof(Product).Name}({id} not found");
        }

        return Task.FromResult(product);
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
        return Task.FromResult(products);

    }

    public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
    {
        return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
    }

    public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> AddAsync(Product entity)
    {
        await _productRepository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProduct();
        return entity;
    }

    public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
    {
        await _productRepository.AddRangeAsync(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllProduct();
        return entities;
    }

    public async Task UpdateAsync(Product entity)
    {
        _productRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProduct();
    }

    public async Task RemoveAsync(Product entity)
    {
        _productRepository.Delete(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProduct();
    }

    public async Task RemoveRangeAsync(IEnumerable<Product> entities)
    {
        _productRepository.RemoveRange(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllProduct();
    }

    public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductByCategory()
    {
        var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);

        var productsWithCategoryDto = _mapper.Map<List<ProductWithCategoryDto>>(products);

        return Task.FromResult(CustomResponseDto<List<ProductWithCategoryDto>>.success(200,productsWithCategoryDto));
    }

    public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductCategory(Guid categoryId)
    {
        throw new NotImplementedException();
    }


    public async Task CacheAllProduct()
    {
        _memoryCache.Set(CacheProductKey, await _productRepository.GetAll().ToListAsync());
    }
}