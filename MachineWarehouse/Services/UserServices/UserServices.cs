using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Repository;
using MachineWarehouse.Models.Request.UserRequestModels;
using Microsoft.EntityFrameworkCore;

namespace MachineWarehouse.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly ApplicationContext _context;

        public UserServices(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task<User> CreateUser(CreateUser request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Login = request.Login,
                Password = request.Password,
                RoleId = _context.Roles.FirstOrDefault(role => role.RoleName == request.RoleName).Id,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }

        public async Task<User> GetUserByid(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

            return user;
        }

        public async Task UpdateUser(UpdateUser request)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.Id);

            entity.Name = request.Name;
            entity.Email = request.Email;
            entity.Login = request.Login;
            entity.Password = request.Password;
            entity.RoleId = _context.Roles.FirstOrDefault(role => role.Id == request.Id).Id;

            await _context.SaveChangesAsync();
        }
    }
}
