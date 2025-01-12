//using BagAPI;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//public static class JwtHelper
//{
//    public static string GenerateJwtToken(Users user, IConfiguration configuration)
//    {
//        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
//        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//        var claims = new[]
//        {
//            new Claim(ClaimTypes.Name, user.name),
//            new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
//        };

//        var token = new JwtSecurityToken(
//            issuer: configuration["Jwt:Issuer"],
//            audience: configuration["Jwt:Audience"],
//            claims: claims,
//            expires: DateTime.Now.AddHours(1),
//            signingCredentials: credentials);

//        return new JwtSecurityTokenHandler().WriteToken(token);
//    }
//}
