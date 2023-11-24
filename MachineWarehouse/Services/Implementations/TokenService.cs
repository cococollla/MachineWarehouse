using MachineWarehouse.Models.Entities;
using MachineWarehouse.Services.Contracts;
using MachineWarehouse.Services.UserServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MachineWarehouse.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        

        public string CreateToken(string role)
        {
            var now = DateTime.UtcNow;
            var claims = new List<Claim> { new Claim(ClaimTypes.Role, role) };

            var jwt = new JwtSecurityToken(
                    issuer: _configuration.GetSection("JWT").GetValue<string>("Issuer"),
                    audience: _configuration.GetSection("JWT").GetValue<string>("Audience"),
                    notBefore: now,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(_configuration.GetSection("JWT").GetValue<double>("Lifetime"))),
                    signingCredentials: CreateSigningCredentials()
                    );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {

            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes("carsupersecret_secretkey!123"));
        }

        public static SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(
                GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256
            );
        }


    }
}
