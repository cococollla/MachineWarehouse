using MachineWarehouse.Models.Entities;
using MachineWarehouse.Services.Contracts;
using MachineWarehouse.Services.Options;
using MachineWarehouse.Services.UserServices;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MachineWarehouse.Services.Implementations
{
    public class TokenService : ITokenService
    {
        public string CreateToken(string role)
        {
            var now = DateTime.UtcNow;
            //var claims = new List<Claim> { new Claim(user.Login, user.Name) };
            var claims = new List<Claim> { new Claim(ClaimTypes.Role, role) };

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    notBefore: now,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                    signingCredentials: AuthOptions.CreateSigningCredentials()
                    );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }


    }
}
