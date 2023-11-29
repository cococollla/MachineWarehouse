using AutoMapper;
using MachineWarehouse.Exceptions;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Repository;
using Microsoft.EntityFrameworkCore;

namespace MachineWarehouse.Services.UserServices
{
    /// <summary>
    /// Сервис для управления записями о пользователе в БД
    /// </summary>
    public class UserServices : IUserServices
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public UserServices(ApplicationContext applicationContext, IMapper mapper)
        {
            _context = applicationContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавляет пользователя в БД
        /// </summary>
        /// <param name="request">Данные пользователя для регистрации</param>
        public async Task CreateUser(User request)
        {
            await _context.Users.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет запись о пользователе из БД
        /// </summary>
        /// <param name="id">Id по которому будет удален пользователь</param>
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

        /// <summary>
        /// Получает список всех пользователей из БД
        /// </summary>
        /// <returns>Список пользователей</returns>
        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users.Include(u => u.Role).ToListAsync();

            return users;
        }

        /// <summary>
        /// Получает запись о пользователе из БД
        /// </summary>
        /// <param name="id">Id по которому будет найден пользователь</param>
        /// <returns>Данные пользователя</returns>
        public async Task<User> GetUserByid(int id)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(user => user.Id == id);

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

        /// <summary>
        /// Получает роль пользователя по его имени
        /// </summary>
        /// <param name="name">Логин по которому будет найден пользователь</param>
        /// <returns>Данные пользователя</returns>
        public async Task<User> GetUserByLogin(string login)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(user => user.Login == login);

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

        /// <summary>
        /// Обновляет данные пользоваетля в БД
        /// </summary>
        /// <param name="request">Обновленные данные</param>
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

        /// <summary>
        /// Получает список всех ролей из БД
        /// </summary>
        /// <returns>Список ролей</returns>
        public async Task<List<Role>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();

            return roles;
        }

        /// <summary>
        /// Проверяет существует ли пользователь с данным логином
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        public async Task<bool> IsExistUser(string login)
        {
            return await _context.Users.AnyAsync(user => user.Login == login);
        }

        /// <summary>
        /// Задает роль по-умолчанию для пользователя при регистрации
        /// </summary>
        /// <returns>Id роли из БД</returns>
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
