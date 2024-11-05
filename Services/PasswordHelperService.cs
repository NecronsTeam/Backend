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
        await passwordRepository.AddPasswordAsync(passwordModel);

        return hashedPassword;
    }

    public async Task<bool> IsPasswordCorrectAsync(string hash, string password)
    {
        var passwordFromDb = await passwordRepository.GetByHashAsync(hash);
        if (passwordFromDb is null)
            return false;

        var decryptedPassword = encryptionService.DecryptString(passwordFromDb.CryptedPassword);
        return password == decryptedPassword;
    }
}
