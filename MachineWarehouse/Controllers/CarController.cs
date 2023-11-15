using AutoMapper;
using MachineWarehouse.Controllers.DtoModels.CarModels;
using MachineWarehouse.Models;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Request.CarRequestModels;
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

        [HttpGet]
        public async Task<ActionResult> GetCars() 
        {
            var cars = await _carServices.GetAllCars();

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCar(int id) 
        {
            await _carServices.GetCarById(id);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCar([FromBody] CreateCarDto car)
        {
            var command = _mapper.Map<CreateCar>(car);
            var carId = await _carServices.AddCar(command);

            return Ok(carId.Id);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCar([FromBody] UpdateCarDto car) 
        {
            var command = _mapper.Map<UpdateCar>(car);
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
