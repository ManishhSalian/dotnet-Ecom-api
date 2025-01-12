using BagAPI.Helper;
using BagAPI.Models;
using BagAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BagAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetAllAsync();
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));

            var response = await _productService.GetByIdAsync(id);
            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }

        // POST: api/products
        [HttpPost]
        [Authorize (Policy = "AdminPolicy")]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (product == null)
                return BadRequest(new ApiResponse<string>(false, "Invalid product data.", null));

            var response = await _productService.CreateAsync(product);
            if (response.Success)
                return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);

            return BadRequest(response);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Update(int id, [FromBody] Product updatedProduct)
        {
            if (id <= 0)
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));

            var response = await _productService.UpdateAsync(id, updatedProduct);
            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));

            var response = await _productService.DeleteAsync(id);
            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }
    }
}
