using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using CrmBackend.Database.Models;
using CrmBackend.Database.Repositories;
using CrmBackend.Api.Dtos;
using CrmBackend.Api.Services;
using CrmBackend.Database.Enums;

namespace CrmBackend.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IConfiguration _configuration, UserRepository userRepository, PasswordHelperService passwordService) : Controller
{
    [HttpPost]
    [Route("register")]
    public async Task RegisterAsync(UserLoginDto dto)
    {
        var isEmailUsed = await userRepository.IsEmailAlreadyUsedAsync(dto.Email);
        if (isEmailUsed)
            throw new BadHttpRequestException("Этот пароль уже использован");

        var hashOfPassword = await passwordService.AddHashedPasswordToDatabaseAsync(dto.Password);

        var user = new User() { Email = dto.Email, HashedPassword = hashOfPassword, Roles = [RolesEnum.Student] };
        await userRepository.CreateEntityAsync(user);
    }

    [HttpPost]
    [Route("login")]
    public async Task<string> LoginAsync(UserLoginDto dto)
    {
        var user = await userRepository.GetUserByEmailAsync(dto.Email) ?? throw new BadHttpRequestException("Не найден пользователь с таким email");
        var isPasswordCorrect = await passwordService.IsPasswordCorrectAsync(user.HashedPassword, dto.Password);
        if (!isPasswordCorrect)
            throw new BadHttpRequestException("Неправильный пароль");

        var claims = GetFilledClaims(user);
        var jwt = CreateJwtToken(claims);

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    private static List<Claim> GetFilledClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.PrimarySid, user.Id.ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
        };

        foreach (var role in user.Roles)
            claims.Add(item: new Claim(ClaimTypes.Role, role.ToString()));

        return claims;
    }

    private JwtSecurityToken CreateJwtToken(List<Claim> claims)
    {
        var authOptions = new AuthOptions(_configuration);

        return new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                signingCredentials: new SigningCredentials(authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
    }

    [Authorize(Roles = "Student")]
    [HttpGet]
    [Route("test")]
    public string Test()
    {
        return "Hello World!";
    }
}
