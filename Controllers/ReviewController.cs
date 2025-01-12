using Microsoft.AspNetCore.Mvc;
using BagAPI.Models;
using BagAPI.Services;
using BagAPI.Helper;
using Microsoft.AspNetCore.Authorization;

namespace BagAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: api/review
        [HttpGet]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetAllReviews()
        {
            var response = await _reviewService.GetAllAsync();
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // GET: api/review/{id}
        [HttpGet("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            var response = await _reviewService.GetByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // POST: api/review
        [HttpPost]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> CreateReview([FromBody] Review review)
        {
            if (review == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid review data.", null));
            }

            var response = await _reviewService.CreateAsync(review);
            if (response.Success)
            {
                return CreatedAtAction(nameof(GetReviewById), new { id = response.Data }, response);
            }

            return BadRequest(response);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] Review review)
        {
            if (review == null)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid review data.", null));
            }

            var response = await _reviewService.UpdateAsync(id, review);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "The 'id' query parameter is required.", null));
            }

            var response = await _reviewService.DeleteAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }


    }



}

