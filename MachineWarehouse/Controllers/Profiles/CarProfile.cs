using AutoMapper;
using MachineWarehouse.Controllers.DtoModels.CarModels;
using MachineWarehouse.Models.Request.CarRequestModels;

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
        }
    }
}
