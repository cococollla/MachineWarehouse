using AutoMapper;
using MachineWarehouse.Exceptions;
using MachineWarehouse.Models.DtoModels;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.View;
using MachineWarehouse.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MachineWarehouse.Controllers
{
    /// <summary>
    /// Контроллер для управления и просмотра пользователей
    /// </summary>
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

        /// <summary>
        /// Загружает форму со списком всех пользователей
        /// </summary>
        [Authorize(Roles = "Admin")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var query = await _userServices.GetAllUsers();
            var users = _mapper.Map<List<UserVm>>(query);

            return View(users);
        }

        /// <summary>
        /// Загружает форму для добавления пользователя
        /// </summary>
        [Authorize(Roles = "Admin")]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            try
            {
                var roles = await _userServices.GetRoles();
                ViewBag.Roles = new SelectList(roles, "Id", "Name");

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The service not available right now" + ex.Message;
                return View();
            }

        }


        /// <summary>
        /// Добавляет нового пользователя в БД
        /// </summary>
        /// <param name="user">Данные пользователя</param>
        [Authorize(Roles = "Admin")]
        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromForm] UserDto user)
        {
            try
            {
                var command = _mapper.Map<User>(user);
                await _userServices.CreateUser(command);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The service not available right now" + ex.Message;
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Обновляет данные пользователя
        /// </summary>
        /// <param name="user">Обновленные данные пользователя</param>
        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateUser")]
        public async Task<ActionResult> UpdateUser([FromForm] UserDto user)
        {
            try
            {
                var command = _mapper.Map<User>(user);
                await _userServices.UpdateUser(command);

                return RedirectToAction("Index");
            }
            catch (NotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The service not available right now" + ex.Message;
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Загружает форму для редактированния выбранного пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserVm>> GetUser(int id)
        {
            try
            {
                var roles = await _userServices.GetRoles();
                ViewBag.Roles = new SelectList(roles, "Id", "Name");
                var query = await _userServices.GetUserByid(id);
                var user = _mapper.Map<UserVm>(query);

                return View(user);
            }
            catch (NotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The service not available right now" + ex.Message;
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Загружает список всех пользователй
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<UserVm>>> GetUsers()
        {
            var query = await _userServices.GetAllUsers();
            var users = _mapper.Map<List<UserVm>>(query);

            return Ok(users);
        }

        /// <summary>
        /// Удаляет пользователя из БД
        /// </summary>
        /// <param name="id">Id выбранного пользователя</param>
        [HttpPost("DeleteUser")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _userServices.DeleteUser(id);
                return RedirectToAction("Index");
            }
            catch (NotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The service not available right now" + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
