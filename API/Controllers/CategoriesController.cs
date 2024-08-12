
using API.CreateDtos;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoriesController : BaseApiController
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryRepository.ListAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);

            return Ok(categoriesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);

            return Ok(categoryDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CategoryCreateDto categoryCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            try
            {
                var newCategory = _mapper.Map<Category>(categoryCreateDto);

                await _categoryRepository.AddAsync(newCategory);

                var category = _mapper.Map<CategoryDto>(newCategory);

                return CreatedAtAction(nameof(GetCategory), new { id = newCategory.Id }, category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, CategoryCreateDto categoryUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                    return NotFound("Category not found");

                _mapper.Map(categoryUpdateDto, category);

                await _categoryRepository.UpdateAsync(category);

                var categoryUpdated = _mapper.Map<CategoryDto>(category);

                return Ok(categoryUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                    return NotFound("Category not found");

                await _categoryRepository.DeleteAsync(category);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}