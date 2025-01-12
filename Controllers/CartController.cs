using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BagAPI.Models;
using BagAPI.Services;
using BagAPI.Helper;


namespace BagAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/cart
        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetAllCarts()
        {
            var response = await _cartService.GetAllAsync();
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // GET: api/cart/{id}
        [HttpGet("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetCartById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }
         

            var response = await _cartService.GetByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // POST: api/cart
        [HttpPost]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> CreateCart([FromBody] Cart cart)
        {
            if (cart == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid cart data.", null));
            }

            var response = await _cartService.CreateAsync(cart);
            if (response.Success)
            {
                return CreatedAtAction(nameof(GetCartById), new { id = response.Data }, response);
            }

            return BadRequest(response);
        }

        // PUT: api/cart/{id}
        [HttpPut("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> UpdateCart(int id, [FromBody] Cart cart)
        {
            if (id <=0 || cart == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid cart data.", null));
            }

            var response = await _cartService.UpdateAsync(id, cart);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // DELETE: api/cart/{id}
        [HttpDelete("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));

            }

            var response = await _cartService.DeleteAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}

