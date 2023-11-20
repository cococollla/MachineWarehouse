using AutoMapper;
using MachineWarehouse.Models.DtoModels;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Request.Car;
using MachineWarehouse.Models.View;
using MachineWarehouse.Profiles.DtoModels.CarModels;

namespace MachineWarehouse.Controllers.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile() 
        {
            CreateMap<Car, CarVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(car => car.Id))
                .ForMember(dest => dest.YearRelese, opt => opt.MapFrom(car => car.YearRelese))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(car => car.Price))
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(car => car.BrandId))
                .ForMember(dest => dest.ColorId, opt => opt.MapFrom(car => car.ColorId))
                .ForMember(dest => dest.ColorName, opt => opt.MapFrom(car => car.Color.Name))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(car => car.Brand.Name))
                .ForMember(dest => dest.ShorDescription, opt => opt.MapFrom(car => car.ShorDescription)).ReverseMap();

            CreateMap<CarDto, CarRequests>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(carDto => carDto.Id))
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(carDto => carDto.BrandId))
                .ForMember(dest => dest.ColorId, opt => opt.MapFrom(carDto => carDto.ColorId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(carDto => carDto.Price))
                .ForMember(dest => dest.YearRelese, opt => opt.MapFrom(carDto => carDto.YearRelese))
                .ForMember(dest => dest.ShorDescription, opt => opt.MapFrom(carDto => carDto.ShorDescription));

            CreateMap<CarRequests, Car>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(carDto => carDto.Id))
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(carDto => carDto.BrandId))
                .ForMember(dest => dest.ColorId, opt => opt.MapFrom(carDto => carDto.ColorId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(carDto => carDto.Price))
                .ForMember(dest => dest.YearRelese, opt => opt.MapFrom(carDto => carDto.YearRelese))
                .ForMember(dest => dest.ShorDescription, opt => opt.MapFrom(carDto => carDto.ShorDescription));

            CreateMap<CarVm, CarRequests>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(carVm => carVm.Id))
                .ForMember(dest => dest.ColorId, opt => opt.MapFrom(carVm => carVm.ColorId))
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(carVm => carVm.BrandId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(carVm => carVm.Price))
                .ForMember(dest => dest.YearRelese, opt => opt.MapFrom(carVm => carVm.YearRelese))
                .ForMember(dest => dest.ShorDescription, opt => opt.MapFrom(carVm => carVm.ShorDescription));

        }
    }
}
