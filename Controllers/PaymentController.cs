using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BagAPI.Models;
using BagAPI.Services;
using BagAPI.Helper;


namespace BagAPI.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: api/payment
        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetAllPayments()
        {
            var response = await _paymentService.GetAllAsync();
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // GET: api/payment/{id}
        [HttpGet("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            var response = await _paymentService.GetByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // POST: api/payment
        [HttpPost]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> CreatePayment([FromBody] Payment payment)
        {
            if (payment == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid payment data.", null));
            }

            var response = await _paymentService.CreateAsync(payment);
            if (response.Success)
            {
                return CreatedAtAction(nameof(GetPaymentById), new { id = response.Data }, response);
            }

            return BadRequest(response);
        }

        // PUT: api/payment/{id}
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] Payment payment)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            if (payment == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid payment data.", null));
            }

            var response = await _paymentService.UpdateAsync(id, payment);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // DELETE: api/payment/{id}
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            var response = await _paymentService.DeleteAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}