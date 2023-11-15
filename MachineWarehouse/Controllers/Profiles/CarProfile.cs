using AutoMapper;
using MachineWarehouse.Controllers.DtoModels.CarModels;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Request.CarRequestModels;
using MachineWarehouse.Models.Request.CarVm;

namespace MachineWarehouse.Controllers.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile() 
        {
            CreateMap<CreateCarDto, CreateCar>()
                .ForMember(dest => dest.YearRelese, opt => opt.MapFrom(carDto => carDto.YearRelese))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(carDto => carDto.Price))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(carDto => carDto.BrandName))
                .ForMember(dest => dest.ColorName, opt => opt.MapFrom(carDto => carDto.ColorName))
                .ForMember(dest => dest.ShorDescription, opt => opt.MapFrom(carDto => carDto.ShorDescription));

            CreateMap<UpdateCarDto, UpdateCar>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(carDto => carDto.Id))
                .ForMember(dest => dest.YearRelese, opt => opt.MapFrom(carDto => carDto.YearRelese))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(carDto => carDto.Price))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(carDto => carDto.BrandName))
                .ForMember(dest => dest.ColorName, opt => opt.MapFrom(carDto => carDto.ColorName))
                .ForMember(dest => dest.ShorDescription, opt => opt.MapFrom(carDto => carDto.ShorDescription));

            CreateMap<Car, GetCarsVm>()
                .ForMember(dest => dest.YearRelese, opt => opt.MapFrom(car => car.YearRelese))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(car => car.Price))
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(car => car.BrandId))
                .ForMember(dest => dest.ColorId, opt => opt.MapFrom(car => car.ColorId))
                .ForMember(dest => dest.ShorDescription, opt => opt.MapFrom(car => car.ShorDescription));

            CreateMap<Car, GetCarVm>()
                .ForMember(dest => dest.YearRelese, opt => opt.MapFrom(car => car.YearRelese))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(car => car.Price))
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(car => car.BrandId))
                .ForMember(dest => dest.ColorId, opt => opt.MapFrom(car => car.ColorId))
                .ForMember(dest => dest.ShorDescription, opt => opt.MapFrom(car => car.ShorDescription));
        }
    }
}
