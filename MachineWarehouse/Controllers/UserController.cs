using AutoMapper;
using MachineWarehouse.Models.Request.UserRequestModels;
using MachineWarehouse.Models.View.User;
using MachineWarehouse.Profiles.DtoModels.UserModels;
using MachineWarehouse.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace MachineWarehouse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public UserController(IUserServices userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateUser([FromBody] UserDto user)
        {
            var command = _mapper.Map<UserRequests>(user);
            var userId = await _userServices.CreateUser(command);

            return Ok(userId.Id);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] UserDto user)
        {
            var command = _mapper?.Map<UserRequests>(user);
            command.Id = user.Id;
            await _userServices.UpdateUser(command);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserVm>> GetUserById(int id)
        {
            var user = await _userServices.GetUserByid(id);

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<UserVm>> GetUsers()
        {
            var vm = await _userServices.GetAllUsers();
            return Ok(vm);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _userServices.DeleteUser(id);
            return NoContent();
        }
    }
}
