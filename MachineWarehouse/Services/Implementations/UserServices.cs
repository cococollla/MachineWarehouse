using AutoMapper;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.View;
using MachineWarehouse.Repository;
using Microsoft.EntityFrameworkCore;

namespace MachineWarehouse.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public UserServices(ApplicationContext applicationContext, IMapper mapper)
        {
            _context = applicationContext;
            _mapper = mapper;
        }

        public async Task<User> CreateUser(User request)
        {
            await _context.Users.AddAsync(request);
            await _context.SaveChangesAsync();

            return request;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if(user == null) 
            {
                throw new Exception("Пользователь не найден");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users.Include(u => u.Role).ToListAsync();

            return users;
        }

        public async Task<User> GetUserByid(int id)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(user => user.Id == id);

            if(user == null)
            {
                throw new Exception("Пользоавтель не найден");
            }

            return user;
        }

        public async Task UpdateUser(User request)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.Id);

            if(entity == null)
            {
                throw new Exception("Пользоавтель не найден");
            }

            entity.Name = request.Name;
            entity.Email = request.Email;
            entity.Login = request.Login;
            entity.Password = request.Password;
            entity.RoleId = request.RoleId;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Role>> GetRoles()
        {
            List<Role> roles = await _context.Roles.ToListAsync();

            return roles;
        }

        public async Task<bool> IsExistUser(string login)
        {
            return await _context.Users.AnyAsync(user => user.Login == login); 
        }
    }
}
