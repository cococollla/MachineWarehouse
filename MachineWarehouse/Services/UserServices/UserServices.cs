using AutoMapper;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Request.UserRequestModels;
using MachineWarehouse.Models.View.User;
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

        public async Task<User> CreateUser(UserRequests request)
        {
            var user = _mapper.Map<User>(request);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
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

        public async Task<List<UserVm>> GetAllUsers()
        {
            var entity = await _context.Users.Include(u => u.Role).ToListAsync();
            var users = _mapper.Map<List<UserVm>>(entity);

            return users;
        }

        public async Task<UserVm> GetUserByid(int id)
        {
            var entity = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(user => user.Id == id);

            if(entity == null)
            {
                throw new Exception("Пользоавтель не найден");
            }

            var user = _mapper.Map<UserVm>(entity);

            return user;
        }

        public async Task UpdateUser(UserRequests request)
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
    }
}
