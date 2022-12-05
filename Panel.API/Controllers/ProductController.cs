using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Panel.API.Filters;
using Panel.DTOs;
using Panel.Repository;
using Panel.Services;

namespace Panel.API.Controllers
{
    public class ProductController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;
        private readonly IProductRepository _repository;

        public ProductController(IMapper mapper, IProductService service, IProductRepository repository)
        {
            _mapper = mapper;
            _service = service;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            var productsDto = _mapper.Map<List<ProductDto>>(products.ToList());
            return CreatActionResult(CustomResponseDto<List<ProductDto>>.success(200, productsDto));
        }
        
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _service.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreatActionResult(CustomResponseDto<ProductDto>.success(200, productsDto));
        }

        [HttpPost()]
        public async Task<IActionResult> CreateNewProduct(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreatActionResult(CustomResponseDto<ProductDto>.success(201, productsDto));
        }
        
        [HttpPut()]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDto productDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(productDto);
            return CreatActionResult(CustomResponseDto<ProductDto>.success(204, productsDto));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);
            return CreatActionResult(CustomResponseDto<NoContentDto>.success(204, new NoContentDto()));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByCategory()
        {
            return CreatActionResult(await _service.GetProductByCategory());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductCategory(Guid categoryId)
        {
            return CreatActionResult(await _service.GetProductCategory(categoryId));
        }
    }
}
