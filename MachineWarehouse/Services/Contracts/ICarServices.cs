using MachineWarehouse.Models;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Request.Car;
using MachineWarehouse.Models.View;
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
        public Task<List<Brand>> GetBrands();
        public Task<List<Color>> GetColors();

    }
}
