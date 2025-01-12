using Microsoft.AspNetCore.Mvc;
using BagAPI.Models;
using BagAPI.Services;
using BagAPI.Helper;
using Microsoft.AspNetCore.Authorization;


namespace BagAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingDetailsController : ControllerBase
    {
        private readonly ShippingDetailService _shippingDetailsService;

        public ShippingDetailsController(ShippingDetailService shippingDetailsService)
        {
            _shippingDetailsService = shippingDetailsService;
        }

        // GET: api/shippingdetails
        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetAllShippingDetails()
        {
            var response = await _shippingDetailsService.GetAllAsync();
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // GET: api/shippingdetails/{id}
        [HttpGet("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetShippingDetailsById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            var response = await _shippingDetailsService.GetByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // POST: api/shippingdetails
        [HttpPost]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> CreateShippingDetails([FromBody] ShippingDetail shippingDetails)
        {
            if (shippingDetails == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid shipping details data.", null));
            }

            var response = await _shippingDetailsService.CreateAsync(shippingDetails);
            if (response.Success)
            {
                return CreatedAtAction(nameof(GetShippingDetailsById), new { id = response.Data }, response);
            }

            return BadRequest(response);
        }

        // PUT: api/shippingdetails/{id}
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateShippingDetails(int id, [FromBody] ShippingDetail shippingDetails)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            if (shippingDetails == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid shipping details data.", null));
            }

            var response = await _shippingDetailsService.UpdateAsync(id, shippingDetails);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // DELETE: api/shippingdetails/{id}
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteShippingDetails(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            var response = await _shippingDetailsService.DeleteAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}