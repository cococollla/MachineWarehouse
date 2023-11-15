using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Request.UserRequestModels;
using MachineWarehouse.Models.Request.UserVm;

namespace MachineWarehouse.Services.UserServices
{
    public interface IUserServices
    {
        public Task<User> CreateUser(CreateUser request);
        public Task UpdateUser(UpdateUser request);
        public Task<List<GetUsersVm>> GetAllUsers();
        public Task<GetUserVm> GetUserByid(int id);
        public Task DeleteUser(int id);
    }
}
