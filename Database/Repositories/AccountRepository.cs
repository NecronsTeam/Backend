using CrmBackend.Database.Models;

namespace CrmBackend.Database.Repositories;

public class AccountRepository(DatabaseContext database) : BaseRepository<Account>(database)
{
    public async Task AttachAvatar(int accountId, Photo avatar)
    {
        var account = await GetEntityByIdAsync(accountId);
        account.Avatar = avatar;

        await _database.SaveChangesAsync();
    }
}
