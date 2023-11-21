using MachineWarehouse.Models.Entities;
using MachineWarehouse.Services.Contracts;
using MachineWarehouse.Services.Options;
using MachineWarehouse.Services.UserServices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MachineWarehouse.Services.Implementations
{
    public class TokenService : ITokenService
    {
        public string CreateToken(User user)
        {
            var claims = new List<Claim> { new Claim(user.Login, user.Name) };

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: AuthOptions.CreateSigningCredentials()
                    );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

    }
}
