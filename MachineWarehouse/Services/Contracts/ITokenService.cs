using MachineWarehouse.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace MachineWarehouse.Services.Contracts
{
    public interface ITokenService
    {
        public string CreateToken(string role);
        public string CreateRefreshToken();
    }
}
