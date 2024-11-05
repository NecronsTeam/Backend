using CrmBackend.Database;
using CrmBackend.Models;

using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Repositories;

public class UserRepository(DatabaseContext database) : BaseRepository<User>(database)
{
    public async Task<bool> IsEmailAlreadyUsedAsync(string emailToCheck)
    {
        return await database.Users.AnyAsync(u => u.Email == emailToCheck);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await database.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
