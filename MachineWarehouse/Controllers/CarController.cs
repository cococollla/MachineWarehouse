using AutoMapper;
using MachineWarehouse.Exceptions;
using MachineWarehouse.Models.DtoModels;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.View;
using MachineWarehouse.Services.CarServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MachineWarehouse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        private readonly ICarServices _carServices;
        private readonly IMapper _mapper;

        public CarController(ICarServices carServices, IMapper mapper)
        {
            _carServices = carServices;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var query = await _carServices.GetAllCars();
            var cars = _mapper.Map<List<CarVm>>(query);

            return View(cars);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            try
            {
                var brands = await _carServices.GetBrands();
                ViewBag.Brands = new SelectList(brands, "Id", "Name");
                var colors = await _carServices.GetColors();
                ViewBag.Colors = new SelectList(colors, "Id", "Name");

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The service nit available right now" + ex.Message;
                return View();
            }

        }

        [HttpGet]
        public async Task<ActionResult<List<CarVm>>> GetCars()
        {
            try
            {
                var query = await _carServices.GetAllCars();
                var cars = _mapper.Map<List<CarVm>>(query);

                return Ok(cars);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The service nit available right now" + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CarVm>> GetCar(int id)
        {
            try
            {
                var brands = await _carServices.GetBrands();
                ViewBag.Brands = new SelectList(brands, "Id", "Name");
                var colors = await _carServices.GetColors();
                ViewBag.Colors = new SelectList(colors, "Id", "Name");
                var query = await _carServices.GetCarById(id);
                var car = _mapper.Map<CarVm>(query);

                return View(car);
            }
            catch (NotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The service nit available right now" + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("CreateCar")]
        public async Task<ActionResult> CreateCar([FromForm] CarDto car)
        {
            try
            {
                var command = _mapper.Map<Car>(car);
                await _carServices.AddCar(command);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The service nit available right now" + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("UpdateCar")]
        public async Task<ActionResult> UpdateCar([FromForm] CarDto car)
        {
            try
            {
                var command = _mapper.Map<Car>(car);
                await _carServices.UpdateCar(command);

                return RedirectToAction("Index");
            }
            catch (NotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The service nit available right now" + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("DeleteCar")]
        public async Task<ActionResult> DeleteCar(int id)
        {
            try
            {
                await _carServices.DeleteCar(id);

                return RedirectToAction("Index");
            }
            catch (NotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "The service nit available right now" + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
