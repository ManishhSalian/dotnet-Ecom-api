using BagAPI.Models;
using BagAPI.Data;

namespace BagAPI.Services
{

    public class CartService : BaseService<Cart>
    {


        public CartService(BagDBContext context, ILogger<CartService> logger) : base(context, logger)
        {

        }
    }
}