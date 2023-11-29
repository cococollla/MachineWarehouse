using AutoMapper;
using MachineWarehouse.Exceptions;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Repository;
using Microsoft.EntityFrameworkCore;

namespace MachineWarehouse.Services.CarServices
{
    /// <summary>
    /// Сервис для управления записями об автомобиле в БД
    /// </summary>
    public class CarServices : ICarServices
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CarServices(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавляет автомобиль в БД
        /// </summary>
        /// <param name="request">Данные автомобиля</param>
        public async Task AddCar(Car request)
        {
            var car = _mapper.Map<Car>(request);

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет запись об автомобиле в БД
        /// </summary>
        /// <param name="id">Id по которому будет найден автомобиль</param>
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

        /// <summary>
        /// Получает список всех автомобилей в БД
        /// </summary>
        /// <returns>Список автомобилей</returns>
        public async Task<List<Car>> GetAllCars()
        {

            var cars = await _context.Cars.Include(b => b.Brand).Include(c => c.Color).ToListAsync();

            return cars;
        }

        /// <summary>
        /// Получает запись об автомобиле из БД
        /// </summary>
        /// <param name="id">Id по которому будет найден автомобиль</param>
        /// <returns>Данные автомобиля</returns>
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

        /// <summary>
        /// Обновляет данные автомобиля в БД
        /// </summary>
        /// <param name="request">Обновленные данные</param>
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

        /// <summary>
        /// Получает список брэндов автомобилей
        /// </summary>
        /// <returns>Список брэндов</returns>
        public async Task<List<Brand>> GetBrands()
        {
            List<Brand> brands = await _context.Brands.ToListAsync();

            return brands;
        }

        /// <summary>
        /// Получает список цветов для автомобилей
        /// </summary>
        /// <returns>Список цветов</returns>
        public async Task<List<Color>> GetColors()
        {
            List<Color> colors = await _context.Colors.ToListAsync();

            return colors;
        }
    }
}
