﻿using AutoMapper;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Repository;
using MachineWarehouse.Models.Request.UserRequestModels;
using MachineWarehouse.Models.Request.UserVm;
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
            var users = _mapper.Map<List<UserVm>>(await _context.Users.ToListAsync());

            return users;
        }

        public async Task<UserVm> GetUserByid(int id)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

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
    }
}
