using CrmBackend.Database;
using CrmBackend.Models;

using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Repositories;

public class PasswordRepository(DatabaseContext database)
{
    public async Task<Password?> GetByHash(string hash)
    {
        return await database.Passwords.Where(p => p.HashedPassword == hash).FirstOrDefaultAsync();
    }

    public async Task AddPassword(Password newPassword)
    {
        await database.Passwords.AddAsync(newPassword);
        await database.SaveChangesAsync();
    }

    public async Task DeleteByHash(string hash)
    {
        var password = await GetByHash(hash);
        if (password is not null)
        {
            database.Passwords.Remove(password);
            await database.SaveChangesAsync();
        }
    }
}
