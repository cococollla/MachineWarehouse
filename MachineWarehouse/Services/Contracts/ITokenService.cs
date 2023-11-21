using MachineWarehouse.Models.Entities;
using Newtonsoft.Json.Linq;

namespace MachineWarehouse.Services.Contracts
{
    public interface ITokenService
    {
        public string CreateToken(User user);
    }
}
