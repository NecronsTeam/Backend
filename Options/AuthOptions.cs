using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CrmBackend.Options;

public class AuthOptions(IConfiguration configuration)
{
    public string Issuer { get; set; } = configuration["Bearer:Issuer"] ?? throw new ArgumentNullException("No issuer found in config");
    public string Audience { get; set; } = configuration["Bearer:Audience"] ?? throw new ArgumentNullException("No audience found in config");
    public string Key { get; set; } = configuration["Bearer:Key"] ?? throw new ArgumentNullException("No key found in config");

    public SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(Key));
}
