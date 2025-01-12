using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BagAPI.Models;
using BagAPI.Services;
using BagAPI.Helper;


namespace BagAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/order
        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await _orderService.GetAllAsync();
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // GET: api/order/{id}
        [HttpGet("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            var response = await _orderService.GetByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // POST: api/order
        [HttpPost]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid order data.", null));
            }

            var response = await _orderService.CreateAsync(order);
            if (response.Success)
            {
                return CreatedAtAction(nameof(GetOrderById), new { id = response.Data }, response);
            }

            return BadRequest(response);
        }

        // PUT: api/order/{id}
        [HttpPut("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            if (order == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid order data.", null));
            }

            var response = await _orderService.UpdateAsync(id, order);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // DELETE: api/order/{id}
        [HttpDelete("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            var response = await _orderService.DeleteAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}