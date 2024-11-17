using CrmBackend.Database.Models;

namespace CrmBackend.Database.Repositories;

public class AccountRepository(DatabaseContext database) : BaseRepository<Account>(database)
{

}
