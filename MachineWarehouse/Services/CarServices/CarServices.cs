using AutoMapper;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Repository;
using MachineWarehouse.Models.Request.CarRequestModels;
using MachineWarehouse.Models.Request.CarVm;
using Microsoft.EntityFrameworkCore;

namespace MachineWarehouse.Services.CarServices
{
    public class CarServices : ICarServices
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CarServices(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Car> AddCar(CarRequests request)
        {
            var car = _mapper.Map<Car>(request);

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return car;
        }

        public async Task DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if(car == null) 
            {
                throw new Exception("Автомобиль не найден");
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CarVm>> GetAllCars()
        {
            var cars =  _mapper.Map<List<CarVm>>(await _context.Cars.ToListAsync());

            return cars;
        }

        public async Task<CarVm> GetCarById(int id)
        {
            var entity = await _context.Cars.FirstOrDefaultAsync(car => car.Id == id);

            if (entity == null) 
            {
                throw new Exception("Автомобиль не найден");
            }

            var car = _mapper.Map<CarVm>(entity);

            return car;
        }

        public async Task UpdateCar(CarRequests request)
        {
            var entity = await _context.Cars.FirstOrDefaultAsync(car => car.Id == request.Id);

            if (entity == null) 
            {
                throw new Exception("Автомобиль не найден");
            }

            entity.YearRelese = request.YearRelese;
            entity.Price = request.Price;
            entity.ShorDescription = request.ShorDescription;
            entity.BrandId = request.BrandId;
            entity.ColorId = request.ColorId;

            await _context.SaveChangesAsync();
        }
    }
}
