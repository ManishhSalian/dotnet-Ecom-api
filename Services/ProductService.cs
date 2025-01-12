using BagAPI.Data;
using BagAPI.Models;

namespace BagAPI.Services
{
    public class ProductService : BaseService<Product>
    {
        public ProductService(BagDBContext context, ILogger<ProductService> logger) : base(context, logger)
        {

        }
    }
}
