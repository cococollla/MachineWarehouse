using AutoMapper;
using MachineWarehouse.Controllers.DtoModels.UserModels;
using MachineWarehouse.Models.Request.UserRequestModels;
using MachineWarehouse.Models.Request.UserVm;
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
        public async Task<ActionResult<int>> CreateUser([FromBody] CreateUserDto user)
        {
            var command = _mapper.Map<CreateUser>(user);
            await _userServices.CreateUser(command);

            return Ok(command);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserDto user)
        {
            var command = _mapper?.Map<UpdateUser>(user);
            command.Id = user.Id;
            await _userServices.UpdateUser(command);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserVm>> GetUserById(int id)
        {
            var user = await _userServices.GetUserByid(id);

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<GetUsersVm>> GetUsers()
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
