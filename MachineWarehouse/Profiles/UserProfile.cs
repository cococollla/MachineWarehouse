using AutoMapper;
using MachineWarehouse.Models.DtoModels.UserModels;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Request.UserRequestModels;
using MachineWarehouse.Models.View;

namespace MachineWarehouse.Controllers.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {

            CreateMap<UserDto, UserRequests>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(userDto => userDto.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(userDto => userDto.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(userDto => userDto.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(userDto => userDto.Password))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(userDto => userDto.Login))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(userDto => userDto.RoleId));

            CreateMap<User, UserVm>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(userDto => userDto.RoleId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom((userDto) => userDto.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(((userDto) => userDto.Name)))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(userDto => userDto.Password))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(userDto => userDto.Login))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(userVm => userVm.Role.Name));

            CreateMap<UserRequests, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(userDto => userDto.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(userDto => userDto.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(userDto => userDto.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(userDto => userDto.Password))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(userDto => userDto.Login))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(userDto => userDto.RoleId));

        }
    }
}
