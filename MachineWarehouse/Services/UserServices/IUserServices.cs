﻿using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Request.UserRequestModels;
using MachineWarehouse.Models.View.User;

namespace MachineWarehouse.Services.UserServices
{
    public interface IUserServices
    {
        public Task<User> CreateUser(UserRequests request);
        public Task UpdateUser(UserRequests request);
        public Task<List<UserVm>> GetAllUsers();
        public Task<UserVm> GetUserByid(int id);
        public Task DeleteUser(int id);
    }
}
