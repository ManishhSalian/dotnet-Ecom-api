using BagAPI.Models;
using BagAPI.Data;


namespace BagAPI.Services
{
    public class CategoryService : BaseService<Category>
    {


        public CategoryService(BagDBContext context, ILogger<CategoryService> logger) : base(context, logger)
        {

        }
    }
}