using Microsoft.EntityFrameworkCore;

namespace CrmBackend.Models;

[Keyless]
public class Password
{
    public required string HashedPassword { get; set; }
    public required string CryptedPassword { get; set; }
}
