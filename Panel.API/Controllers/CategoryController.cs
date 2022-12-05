using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Panel.DTOs;
using Panel.Services;

namespace Panel.API.Controllers
{
    public class CategoryController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Category> _service;
        private readonly ICategoryService _categoryService;
        
        public CategoryController(IMapper mapper, IService<Category> service, ICategoryService categoryService)
        {
            _mapper = mapper;
            _service = service;
            _categoryService = categoryService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateNewCategory(CategoryDto categoryDto)
        {
            var category = await _service.AddAsync(_mapper.Map<Category>(categoryDto));
            var categoriesDto = _mapper.Map<CategoryDto>(category);
            return CreatActionResult(CustomResponseDto<CategoryDto>.success(201, categoriesDto));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _service.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
            return CreatActionResult(CustomResponseDto<List<CategoryDto>>.success(200, categoriesDto));
        }
        
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetCategoryWithProductsByIdAsync(Guid id)
        {
            return CreatActionResult(await _categoryService.GetCategoryWithProductsByIdAsync(id));
        }
    }
}
