using BagAPI.Data;
using BagAPI.Models;

namespace BagAPI.Services
{
    public class StockService : BaseService<Stock>
    {
        public StockService(BagDBContext context, ILogger<StockService> logger) : base(context, logger)
        {

        }
    }
}