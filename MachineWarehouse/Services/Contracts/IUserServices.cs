using MachineWarehouse.Models.Entities;

namespace MachineWarehouse.Services.UserServices
{
    public interface IUserServices
    {
        public Task<User> CreateUser(User request);
        public Task UpdateUser(User request);
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserByid(int id);
        public Task DeleteUser(int id);
        public Task<List<Role>> GetRoles();
        public Task<bool> IsExistUser(string login);
        public Task<User> GetUserByName(string name);
        public int GetDefaultRole();
    }
}
