using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Request.UserRequestModels;

namespace MachineWarehouse.Services.UserServices
{
    public interface IUserServices
    {
        public Task<User> CreateUser(CreateUser request);
        public Task UpdateUser(UpdateUser request);
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserByid(int id);
        public Task DeleteUser(int id);
    }
}
