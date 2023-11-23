using MachineWarehouse.Models.Entities;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace MachineWarehouse.Services.Contracts
{
    public interface ITokenService
    {
        public string CreateToken(string role);
    }
}
