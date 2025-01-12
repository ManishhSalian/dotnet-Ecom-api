using BagAPI.Models;
using BagAPI.Data;

namespace BagAPI.Services
{
    public class CartItemService : BaseService<CartItem>
    {


        public CartItemService(BagDBContext context, ILogger<CartItemService> logger) : base(context, logger)
        {

        }
    }
}