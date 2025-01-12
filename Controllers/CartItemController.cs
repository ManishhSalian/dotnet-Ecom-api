using Microsoft.AspNetCore.Authorization;
using BagAPI.Models;
using BagAPI.Services;
using BagAPI.Helper;
using Microsoft.AspNetCore.Mvc;


namespace BagAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemController : ControllerBase
    {
        private readonly CartItemService _cartItemService;

        public CartItemController(CartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        // GET: api/cartitem
        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetAllCartItems()
        {
            var response = await _cartItemService.GetAllAsync();
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // GET: api/cartitem/{id}
        [HttpGet("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetCartItemById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            var response = await _cartItemService.GetByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // POST: api/cartitem
        [HttpPost]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> CreateCartItem([FromBody] CartItem cartItem)
        {
            if (cartItem == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid cart item data.", null));
            }

            var response = await _cartItemService.CreateAsync(cartItem);
            if (response.Success)
            {
                return CreatedAtAction(nameof(GetCartItemById), new { id = response.Data }, response);
            }

            return BadRequest(response);
        }

        // PUT: api/cartitem/{id}
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateCartItem(int id, [FromBody] CartItem cartItem)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            if (cartItem == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid cart item data.", null));
            }

            var response = await _cartItemService.UpdateAsync(id, cartItem);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // DELETE: api/cartitem/{id}
        [HttpDelete("{id}")]
        [Authorize (Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            var response = await _cartItemService.DeleteAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}