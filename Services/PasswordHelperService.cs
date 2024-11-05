using System.Security.Cryptography;
using System.Text;

using CrmBackend.Dtos;
using CrmBackend.Models;
using CrmBackend.Repositories;

namespace CrmBackend.Services;

public class PasswordHelperService(EncryptionService encryptionService, PasswordRepository passwordRepository)
{
    private static readonly SHA256 sha256 = SHA256.Create();

    public static string GetPasswordHash(string yourString)
    {
        return string.Join("", MD5.HashData(Encoding.ASCII.GetBytes(yourString)).Select(s => s.ToString("x2")));
    }

    public async Task<string> AddHashedPasswordToDatabaseAsync(string password)
    {
        var hashedPassword = GetPasswordHash(password);
        var cryptedPassword = encryptionService.EncryptString(password);

        var passwordModel = new Password() { HashedPassword = hashedPassword, CryptedPassword = cryptedPassword };
        await passwordRepository.AddPassword(passwordModel);

        return hashedPassword;
    }

    public async Task<bool> IsPasswordCorrectAsync(User user, UserLoginDto userLoginInfo)
    {
        var passwordHash = GetPasswordHash(userLoginInfo.Password);
        var passwordFromDatabaseObject = await passwordRepository.GetByHash(passwordHash);
        if (passwordFromDatabaseObject is null)
            return false;

        var decryptedPassword = encryptionService.DecryptString(passwordFromDatabaseObject.CryptedPassword);
        return userLoginInfo.Password == decryptedPassword;
    }
}
