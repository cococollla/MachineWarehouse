﻿using AutoMapper;
using MachineWarehouse.Models.DtoModels;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Identuty;
using MachineWarehouse.Models.View;
using MachineWarehouse.Services.Contracts;
using MachineWarehouse.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace MachineWarehouse.Controllers
{
    /// <summary>
    /// Контроллер для регистрации и авторизации
    /// </summary>
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

        /// <summary>
        /// Создает access и refresh токены
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Созданные токены и роль пользователя</returns>
        [HttpPost("Token")]
        private async Task<AuthResponse> GetToken(string login)
        {
            var command = await _userService.GetUserByLogin(login);//Поиск роли для дальнейшего добавления в Clims
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

        /// <summary>
        /// Загружает форму входа
        /// </summary>
        [HttpGet("Auth")]
        public IActionResult Authenticate()
        {
            return View();
        }

        /// <summary>
        /// Проверяет данные пользовател для входа
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>Созданные токены и роль пользователя</returns>
        [HttpPost("Authenticate")]
        public async Task<IResult> Authenticate([FromForm] string login, [FromForm] string password)
        {
            var isExist = await _userService.IsExistUser(login);

            if (!isExist)
            {
                return Results.BadRequest("User not exist");
            }

            var response = await GetToken(login);

            return Results.Json(response);
        }

        /// <summary>
        /// Загружает форму регистрации
        /// </summary>
        [HttpGet("Registration")]
        public IActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// Добавляет зарегистрированного пользователя в БД
        /// </summary>
        /// <param name="user">Данные пользователя</param>
        [HttpPost("Registration")]
        public async Task<ActionResult> Registration([FromForm] UserDto user)
        {
            var isExist = await _userService.IsExistUser(user.Login);

            if (!isExist)
            {
                user.RoleId = _userService.GetDefaultRole();
                var command = _mapper.Map<User>(user);
                await _userService.CreateUser(command);

                return RedirectToAction("Authenticate");
            }

            return View(user);

        }
    }
}
