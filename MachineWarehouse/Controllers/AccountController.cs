using AutoMapper;
using MachineWarehouse.Models.DtoModels;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Identuty;
using MachineWarehouse.Models.View;
using MachineWarehouse.Services.Contracts;
using MachineWarehouse.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace MachineWarehouse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IUserServices _userService;
        private readonly IMapper _mapper;

        public AccountController(ITokenService tokenService, IUserServices userService, IMapper mapper)
        {
            _tokenService = tokenService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("Token")]
        public async Task<IResult> GetToken(UserDto user)
        {
            var command = await _userService.GetUserByName(user.Name);
            var findUser = _mapper.Map<UserVm>(command);
            var token = _tokenService.CreateToken(findUser.Role);

            var response = new AuthResponse
            {
                Role = findUser.Role,
                Token = token,
            };

            return Results.Json(response);
        }

        [HttpGet("Authenticate")]
        public IActionResult Authenticate()
        {
            return View();
        }

        [HttpPost("Authenticate")]
        public async Task<IResult> Authenticate([FromForm] UserDto user)
        {
            var isExist = await _userService.IsExistUser(user.Login);

            if (!isExist)
            {
                //return RedirectToAction("Registration");
                return Results.BadRequest("User not exist");
            }

            var command = await _userService.GetUserByName(user.Name);
            var findUser = _mapper.Map<UserVm>(command);
            var token = _tokenService.CreateToken(findUser.Role);

            var response = new AuthResponse
            {
                Role = findUser.Role,
                Token = token,
            };

            //return RedirectToAction("Index", "Car");
            return Results.Json(response);
        }



        [HttpGet("Registration")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost("Registration")]
        public async Task<ActionResult> Registration([FromForm] UserDto user)
        {
            var isExist = await _userService.IsExistUser(user.Login);
            user.RoleId = 3;
            var command = _mapper.Map<User>(user);

            if (!isExist)
            {
                await _userService.CreateUser(command);

                return RedirectToAction("Authenticate");
            }

            return View(user);

        }
    }
}
