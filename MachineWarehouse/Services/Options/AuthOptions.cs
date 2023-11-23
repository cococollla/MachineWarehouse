using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MachineWarehouse.Services.Options
{
    public class AuthOptions
    {
        public const string Issuer = "http://localhost:7000";
        public const string Audience = "http://localhost:7001";
        private const string Key = "carsupersecret_secretkey!123";
        public const int Lifetime = 1;

        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

        public static SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(
                GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256
            );
        }
    }
}
