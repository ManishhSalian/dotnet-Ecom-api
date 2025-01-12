using Microsoft.AspNetCore.Mvc;
using BagAPI.Models;
using BagAPI.Services;
using BagAPI.Helper;
using Microsoft.AspNetCore.Authorization;

namespace BagAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly StockService _stockerService;

        public StockController(StockService stockerService)
        {
            _stockerService = stockerService;
        }

        // GET: api/stocker
        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetAllStockers()
        {
            var response = await _stockerService.GetAllAsync();
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // GET: api/stocker/{id}
        [HttpGet("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetStockerById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            var response = await _stockerService.GetByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // POST: api/stocker
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateStocker([FromBody] Stock stocker)
        {
            if (stocker == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid stocker data.", null));
            }

            var response = await _stockerService.CreateAsync(stocker);
            if (response.Success)
            {
                return CreatedAtAction(nameof(GetStockerById), new { id = response.Data }, response);
            }

            return BadRequest(response);
        }

        [HttpPut("{id}")]   
        [Authorize (Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateStocker(int id, [FromBody] Stock stocker)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            if (stocker == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid stocker data.", null));
            }

            var response = await _stockerService.UpdateAsync(id, stocker);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // DELETE: api/stocker/{id}
        [HttpDelete("{id}")]
        [Authorize (Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteStocker(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            var response = await _stockerService.DeleteAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }   
    }
}