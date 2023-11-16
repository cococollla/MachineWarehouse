using MachineWarehouse.Models;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Request.CarRequestModels;
using MachineWarehouse.Models.Request.CarVm;
using System.Threading.Tasks;

namespace MachineWarehouse.Services.CarServices
{
    public interface ICarServices
    {
        public Task<Car> AddCar(CarRequests request);
        public Task<List<CarVm>> GetAllCars();
        public Task<CarVm> GetCarById(int id);
        public Task UpdateCar(CarRequests request);
        public Task DeleteCar(int id);

    }
}
