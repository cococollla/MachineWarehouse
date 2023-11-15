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

        public async Task<Car> AddCar(CreateCar request)
        {
            var car = new Car
            {
                YearRelese = request.YearRelese,
                Price = request.Price,
                ShorDescription = request.ShorDescription,
                BrandId = _context.Brands.FirstOrDefault(brand => brand.Brand == request.BrandName).Id,
                ColorId = _context.Colors.FirstOrDefault(color => color.Color == request.ColorName).Id,
            };

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return car;
        }

        public async Task DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GetCarsVm>> GetAllCars()
        {
            var cars =  _mapper.Map<List<GetCarsVm>>(await _context.Cars.ToListAsync());

            return cars;
        }

        public async Task<GetCarVm> GetCarById(int id)
        {
            var car = _mapper.Map<GetCarVm>(await _context.Cars.FirstOrDefaultAsync(car => car.Id == id));

            return car;
        }

        public async Task UpdateCar(UpdateCar request)
        {
            var entity = await _context.Cars.FirstOrDefaultAsync(car => car.Id == request.Id);

            entity.YearRelese = request.YearRelese;
            entity.Price = request.Price;
            entity.ShorDescription = request.ShorDescription;
            entity.BrandId = _context.Brands.FirstOrDefault(brand => brand.Brand == request.BrandName).Id;
            entity.ColorId = _context.Colors.FirstOrDefault(color => color.Color == request.ColorName).Id;

            await _context.SaveChangesAsync();
        }
    }
}
