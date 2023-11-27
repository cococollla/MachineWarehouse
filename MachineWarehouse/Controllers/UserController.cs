using AutoMapper;
using MachineWarehouse.Models.DtoModels;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.View;
using MachineWarehouse.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var query = await _userServices.GetAllUsers();
            var users = _mapper.Map<List<UserVm>>(query);

            return View(users);
        }

        [Authorize(Roles = "Admin")]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            var roles = await _userServices.GetRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromForm] UserDto user)
        {
            var command = _mapper.Map<User>(user);
            await _userServices.CreateUser(command);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateUser")]
        public async Task<ActionResult> UpdateUser([FromForm] UserDto user)
        {
            var command = _mapper.Map<User>(user);
            await _userServices.UpdateUser(command);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserVm>> GetUser(int id)
        {
            var roles = await _userServices.GetRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            var query = await _userServices.GetUserByid(id);
            var user = _mapper.Map<UserVm>(query);

            return View(user);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserVm>>> GetUsers()
        {
            var query = await _userServices.GetAllUsers();
            var users = _mapper.Map<List<UserVm>>(query);

            return Ok(users);
        }

        [HttpPost("DeleteUser")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userServices.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
