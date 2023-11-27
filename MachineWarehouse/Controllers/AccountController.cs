using AutoMapper;
using MachineWarehouse.Models.DtoModels;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Identuty;
using MachineWarehouse.Models.View;
using MachineWarehouse.Services.Contracts;
using MachineWarehouse.Services.UserServices;
using Microsoft.AspNetCore.Http;
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
        public async Task<AuthResponse> GetToken(UserDto user)
        {
            var command = await _userService.GetUserByName(user.Name);//Поиск роли для дальнейшего добавления в Clims
            var findUser = _mapper.Map<UserVm>(command);
            var token = _tokenService.CreateToken(findUser.Role);
            var refreshToken = _tokenService.CreateRefreshToken();

            var cookieOptions = new CookieOptions //добавление refreshToken в куки на неделю
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);

            var response = new AuthResponse
            {
                Role = findUser.Role,
                AccessToken = token,
                RefreshToken = refreshToken
            };

            return response;
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
                return Results.BadRequest("User not exist");
            }

            var response = await GetToken(user);

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

        //private IActionResult UpdateAccessToken(HttpContext context)
        //{
        //    string? refreshToken = Request.Cookies["refreshToken"];
        //    string? role = Request.Cookies["role"];
        //    //Если refresh token истек логинимся заново
        //    if (refreshToken == null)
        //    {
        //        return RedirectToAction("Authenticate");
        //    }              

        //    var acceessToken = _tokenService.CreateToken(role);
        //    Response.Cookies.Append("acceessToken", acceessToken);

        //    return ;
        //}
    }
}
