using System.Security.Cryptography;

namespace CrmBackend.Api.Services;

public class EncryptionService(IConfiguration configuration)
{
    private byte[] Key { get; set; } = Convert.FromBase64String(configuration["Encryption:Key"] ?? throw new KeyNotFoundException("Set encryption key in config"));
    private byte[] Iv { get; set; } = Convert.FromBase64String(configuration["Encryption:Iv"] ?? throw new KeyNotFoundException("Set encryption iv in config"));

    /// <summary>
    ///     Encrypts the specified plain text using the AES algorithm.
    /// </summary>
    /// <param name="plainText">The plain text to encrypt.</param>
    /// <returns>The encrypted cipher text.</returns>
    public string EncryptString(string plainText)
    {
        using Aes aes = Aes.Create();
        //aes.Key = Key;
        //aes.IV = Iv;

        var encryptor = aes.CreateEncryptor(Key, Iv);

        using var msEncrypt = new MemoryStream();

        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            using var swEncrypt = new StreamWriter(csEncrypt);
            swEncrypt.Write(plainText);
        }

        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    /// <summary>
    ///     Decrypts the specified cipher text using the AES algorithm.
    /// </summary>
    /// <param name="cipherText">The cipher text to decrypt.</param>
    /// <returns>The decrypted plain text.</returns>
    public string DecryptString(string cipherText)
    {
        var cipherBytes = Convert.FromBase64String(cipherText);

        using var aesAlg = Aes.Create();
        aesAlg.Key = Key;
        aesAlg.IV = Iv;

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var msDecrypt = new MemoryStream(cipherBytes);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);

        return srDecrypt.ReadToEnd();
    }
}