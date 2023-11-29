using AutoMapper;
using MachineWarehouse.Exceptions;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Repository;
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

        public async Task<Car> AddCar(Car request)
        {

            var car = _mapper.Map<Car>(request);

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return car;
        }

        public async Task DeleteCar(int id)
        {
            try
            {
                var car = await _context.Cars.FindAsync(id);

                if (car == null)
                {
                    throw new NotFoundException($"Car is not found");
                }

                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

        }

        public async Task<List<Car>> GetAllCars()
        {

            var cars = await _context.Cars.Include(b => b.Brand).Include(c => c.Color).ToListAsync();

            return cars;
        }

        public async Task<Car> GetCarById(int id)
        {
            try
            {
                var car = await _context.Cars.Include(b => b.Brand).Include(c => c.Color).FirstOrDefaultAsync(car => car.Id == id);

                if (car == null)
                {
                    throw new NotFoundException($"Car is not found");
                }

                return car;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateCar(Car request)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(car => car.Id == request.Id);

                if (car == null)
                {
                    throw new NotFoundException($"Car is not found");
                }

                car.YearRelese = request.YearRelese;
                car.Price = request.Price;
                car.ShorDescription = request.ShorDescription;
                car.BrandId = request.BrandId;
                car.ColorId = request.ColorId;

                await _context.SaveChangesAsync();
            }
            catch
            {

            }
        }

        public async Task<List<Brand>> GetBrands()
        {
            List<Brand> brands = await _context.Brands.ToListAsync();

            return brands;
        }

        public async Task<List<Color>> GetColors()
        {
            List<Color> colors = await _context.Colors.ToListAsync();

            return colors;
        }
    }
}
