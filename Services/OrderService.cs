using BagAPI.Data;
using BagAPI.Models;

namespace BagAPI.Services
{
    public class OrderService : BaseService<Order>
    {

        public OrderService(BagDBContext context, ILogger<OrderService> logger) : base(context, logger)
        {

        }
    }
}