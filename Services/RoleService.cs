using BagAPI.Data;
using BagAPI.Models;
using Microsoft.Extensions.Logging;

namespace BagAPI.Services
{
    public class RoleService : BaseService<Role>
    {
        public RoleService(BagDBContext context, ILogger<RoleService> logger) : base(context, logger)
        {
        }
    }
}