using System.Security.Claims;

using CrmBackend.Database.Models;
using CrmBackend.Database.Repositories;

namespace CrmBackend.Api.Helpers;

public static class ClaimsPrincipalExtensions
{
    public static async Task<User> GetUser(this ClaimsPrincipal claimsPrincipal, UserRepository userRepository)
    {
        var id = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid)?.Value
            ?? throw new UnauthorizedAccessException("Не получилось получить пользователя из Claims");

        return await userRepository.GetEntityByIdAsync(int.Parse(id));
    }
}
