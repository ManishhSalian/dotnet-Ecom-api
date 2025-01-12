using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BagAPI.Services;
using BagAPI.Data;
using System.Security.Claims;
using BagAPI.Models;
using Microsoft.EntityFrameworkCore;
using BagAPI.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BagAPI.Services
{
    public class UserService : BaseService<User>
    {
        private readonly IConfiguration _configuration;

        public UserService(BagDBContext context, IConfiguration configuration, ILogger<UserService> logger) : base(context, logger)
        {
            _configuration = configuration;
        }

        public string Authenticate(string email, string password)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == email);

            if (user == null || user.Password != password) // Check if user is null or password is incorrect
            {
                _logger.LogWarning("Authentication failed: Username or password is incorrect.");
                return null;
            }

            if (user.Role == null)
            {
                _logger.LogWarning($"Authentication failed: Role missing for user {email}.");
                return null;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var tokenExpiryConfig = _configuration["JwtSettings:TokenExpiryInHours"];
            var tokenExpiry = string.IsNullOrEmpty(tokenExpiryConfig) ? 1 : int.Parse(tokenExpiryConfig); // Default to 1 hour

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(tokenExpiry),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ApiResponse<List<User>>> GetUsersByRoleIdAsync(int roleId)
        {
            try
            {
                var users = await _context.Users
                    .Where(u => u.RoleId == roleId)
                    .ToListAsync();

                if (users == null || !users.Any())
                {
                    return new ApiResponse<List<User>>(false, "No users found for the given role.", null);
                }

                return new ApiResponse<List<User>>(true, "Users fetched successfully.", users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching users by role ID");
                return new ApiResponse<List<User>>(false, ex.Message, null);
            }
        }
    }
}