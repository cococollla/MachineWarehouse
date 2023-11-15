using MachineWarehouse.Models;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Services.CarServices;
using Microsoft.AspNetCore.Mvc;

namespace MachineWarehouse.Controllers
{
    [Controller]
    public class CarController : Controller
    {
        private readonly ICarServices _carServices;

        public CarController(ICarServices carServices)
        {
            _carServices = carServices;
        }

        [HttpGet]
        public IActionResult GetCars() 
        {
            var cars = _carServices.GetAllCars();

            return View(cars);
        }

        [HttpGet]
        public IActionResult GetCar(int id) 
        {
            _carServices.GetCarById(id);
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateCar([FromBody]Car car)
        {
            //создать dto модель и сделать мапинг ее с request

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateCar([FromBody]Car car) 
        {
            //создать dto модель и сделать мапинг ее с request
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteCar(int id) 
        {
            _carServices.DeleteCar(id);
            return Ok();
        }
    }
}
