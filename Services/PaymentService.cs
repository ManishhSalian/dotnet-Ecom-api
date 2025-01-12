using BagAPI.Data;
using BagAPI.Models;


namespace BagAPI.Services
{
    public class PaymentService : BaseService<Payment>
    {
        public PaymentService(BagDBContext context, ILogger<PaymentService> logger) : base(context, logger)
        {

        }
    }
}