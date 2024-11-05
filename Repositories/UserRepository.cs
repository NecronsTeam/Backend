using CrmBackend.Database;
using CrmBackend.Models;

namespace CrmBackend.Repositories;

public class UserRepository(DatabaseContext database) : BaseRepository<User>(database)
{

}
