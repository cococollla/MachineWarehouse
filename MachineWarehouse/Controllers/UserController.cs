using AutoMapper;
using MachineWarehouse.Models.Request.UserRequestModels;
using MachineWarehouse.Models.View.User;
using MachineWarehouse.Profiles.DtoModels.UserModels;
using MachineWarehouse.Services.UserServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [Route("index")]
        public async Task<IActionResult> Index()
        {
            var users = await _userServices.GetAllUsers();
            return View(users);
        }

        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            var roles = await _userServices.GetRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            return View();
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromForm] UserDto user)
        {
            var command = _mapper.Map<UserRequests>(user);
            var userId = await _userServices.CreateUser(command);

            return RedirectToAction("Index");
        }

        [HttpPost("UpdateUser")]
        public async Task<ActionResult> UpdateUser([FromForm] UserDto user)
        {
            var command = _mapper.Map<UserRequests>(user);
            await _userServices.UpdateUser(command);

            return RedirectToAction("Index");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserVm>> GetUser(int id)
        {
            var roles = await _userServices.GetRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            var user = await _userServices.GetUserByid(id);

            return View(user);
        }

        [HttpGet]
        public async Task<ActionResult<UserVm>> GetUsers()
        {
            var vm = await _userServices.GetAllUsers();
            return Ok(vm);
        }

        [HttpPost("DeleteUser")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userServices.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
