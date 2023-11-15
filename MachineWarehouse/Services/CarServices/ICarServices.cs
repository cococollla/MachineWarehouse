using MachineWarehouse.Models;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Request.CarRequestModels;
using System.Threading.Tasks;

namespace MachineWarehouse.Services.CarServices
{
    public interface ICarServices
    {
        public Task<Car> AddCar(CreateCar request);
        public Task<List<Car>> GetAllCars();
        public Task<Car> GetCarById(int id);
        public Task UpdateCar(UpdateCar request);
        public Task DeleteCar(int id);

    }
}
