using AutoMapper;
using MachineWarehouse.Models.DtoModels;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Identuty;
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

        [HttpGet("Authenticate")]
        public IActionResult Authenticate()
        {
            return View();
        }

        [HttpPost("Authenticate")]
        public async Task<ActionResult> Authenticate([FromForm] UserDto user)
        {
            var isExist = await _userService.IsExistUser(user.Login);
            var command = _mapper.Map<User>(user);

            if (!isExist)
            {
                return RedirectToAction("Registration");
            }

            var token = _tokenService.CreateToken(command);

            return Ok(new AuthResponse
            {
                Name = user.Name,
                Login = user.Login,
                Token = token,
            });

            //return Results.Json(token);
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
