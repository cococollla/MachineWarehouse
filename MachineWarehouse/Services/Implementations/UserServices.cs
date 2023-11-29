using AutoMapper;
using MachineWarehouse.Exceptions;
using MachineWarehouse.Models.Entities;
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
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    throw new NotFoundException("User is not found");
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users.Include(u => u.Role).ToListAsync();

            return users;
        }

        public async Task<User> GetUserByid(int id)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(user => user.Id == 40);

                if (user == null)
                {
                    throw new NotFoundException("User is not found");
                }

                return user;
            }
            catch
            {
                throw;
            }

        }

        public async Task<User> GetUserByName(string name)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(user => user.Name == name);

                if (user == null)
                {
                    throw new NotFoundException("User is not found");
                }

                return user;
            }
            catch
            {
                throw;
            }

        }

        public async Task UpdateUser(User request)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.Id);

                if (user == null)
                {
                    throw new NotFoundException("User is not found");
                }

                user.Name = request.Name;
                user.Email = request.Email;
                user.Login = request.Login;
                user.Password = request.Password;
                user.RoleId = request.RoleId;

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Role>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();

            return roles;
        }

        public async Task<bool> IsExistUser(string login)
        {
            return await _context.Users.AnyAsync(user => user.Login == login);
        }

        public int GetDefaultRole()
        {
            try
            {
                var roleId = _context.Roles.FirstOrDefaultAsync(role => role.Name == "User").Id;

                if (roleId == null)
                {
                    throw new NotFoundException("User is not found");
                }

                return roleId;
            }
            catch
            {
                throw;
            }
        }
    }
}
