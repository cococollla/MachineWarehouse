using AutoMapper;
using MachineWarehouse.Models.DtoModels;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.View;
using MachineWarehouse.Services.CarServices;
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

        [Route("index")]
        public async Task<IActionResult> Index()
        {
            var query = await _carServices.GetAllCars();
            var cars = _mapper.Map<List<CarVm>>(query);

            return View(cars);
        }

        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            var brands= await _carServices.GetBrands();
            ViewBag.Brands = new SelectList(brands, "Id", "Name");

            var colors = await _carServices.GetColors();
            ViewBag.Colors = new SelectList(colors, "Id", "Name");

            return View();
        }

        [HttpGet]
        public async Task<ActionResult<List<CarVm>>> GetCars() 
        {
            var query = await _carServices.GetAllCars();
            var cars = _mapper.Map<List<CarVm>>(query);

            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarVm>> GetCar(int id)
        {
            var brands = await _carServices.GetBrands();
            ViewBag.Brands = new SelectList(brands, "Id", "Name");

            var colors = await _carServices.GetColors();
            ViewBag.Colors = new SelectList(colors, "Id", "Name");

            var query = await _carServices.GetCarById(id);
            var car = _mapper.Map<CarVm>(query);

            return View(car);
        }

        [HttpPost("CreateCar")]
        public async Task<ActionResult> CreateCar([FromForm] CarDto car)
        {
            var command = _mapper.Map<Car>(car);
            await _carServices.AddCar(command);

            return RedirectToAction("Index");
        }


        [HttpPost("UpdateCar")]
        public async Task<ActionResult> UpdateCar([FromForm] CarDto car) 
        {
            var command = _mapper.Map<Car>(car);
            command.Id = car.Id;
            await _carServices.UpdateCar(command);

            return RedirectToAction("Index");
        }

        [HttpPost("DeleteCar")]
        public async Task<ActionResult> DeleteCar(int id) 
        {
            await _carServices.DeleteCar(id);

            return RedirectToAction("Index");
        }
    }
}
