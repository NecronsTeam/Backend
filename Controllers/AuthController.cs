using CrmBackend.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace CrmBackend.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IConfiguration _configuration)
{
    [HttpPost]
    [Route("login")]
    public string Login(string username)
    {
        var claims = new List<Claim> { new(ClaimTypes.Name, username), new(ClaimTypes.Role, "Some_role") };
        // создаем JWT-токен
        var authOptions = new AuthOptions(_configuration);

        var jwt = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(7)),
                signingCredentials: new SigningCredentials(authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    [Authorize]
    [HttpGet]
    [Route("test")]
    public string Test()
    {
        return "Hello World!";
    }
}
