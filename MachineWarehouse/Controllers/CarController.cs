using AutoMapper;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Request.CarRequestModels;
using MachineWarehouse.Models.Request.CarVm;
using MachineWarehouse.Profiles.DtoModels.CarModels;
using MachineWarehouse.Services.CarServices;
using Microsoft.AspNetCore.Mvc;

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
            var Cars = _mapper.Map<List<Car>>(await _carServices.GetAllCars());
            return View(Cars);
        }

        [HttpGet]
        public async Task<ActionResult<CarVm>> GetCars() 
        {
            var cars = await _carServices.GetAllCars();

            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarVm>> GetCar(int id) 
        {
            var car = await _carServices.GetCarById(id);
            return Ok(car);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCar([FromBody] CarDto car)
        {
            var command = _mapper.Map<CarRequests>(car);
            var carId = await _carServices.AddCar(command);

            return Ok(carId.Id);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCar([FromBody] CarDto car) 
        {
            var command = _mapper.Map<CarRequests>(car);
            command.Id = car.Id;
            await _carServices.UpdateCar(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCar(int id) 
        {
            await _carServices.DeleteCar(id);
            return Ok();
        }
    }
}
