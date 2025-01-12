using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BagAPI.Models;
using BagAPI.Services;
using BagAPI.Helper;

namespace BagAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/category
        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _categoryService.GetAllAsync();
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // GET: api/category/{id}
        [HttpGet("{id:int}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            var response = await _categoryService.GetByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // POST: api/category
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid category data.", null));
            }

            var response = await _categoryService.CreateAsync(category);
            if (response.Success)
            {
                return CreatedAtAction(nameof(GetCategoryById), new { id = response.Data }, response);
            }

            return BadRequest(response);
        }

        // PUT: api/category/{id}
        [HttpPut("{id:int}")]
        [Authorize(Policy = "AdminPolicy")] 
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            if (category == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid category data.", null));
            }

            var response = await _categoryService.UpdateAsync(id, category);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // DELETE: api/category/{id}
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            var response = await _categoryService.DeleteAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}