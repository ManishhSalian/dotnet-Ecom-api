using BagAPI.Models;
using BagAPI.Data;

namespace BagAPI.Services
{
    public class ShippingDetailService : BaseService<ShippingDetail>
    {
        public ShippingDetailService(BagDBContext context, ILogger<ShippingDetailService> logger) : base(context, logger)
        {

        }
    }
}