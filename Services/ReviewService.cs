using BagAPI.Data;
using BagAPI.Models;

namespace BagAPI.Services
{
    public class ReviewService : BaseService<Review>
    {
        public ReviewService(BagDBContext context, ILogger<ReviewService> logger) : base(context, logger)
        {

        }
    }
}