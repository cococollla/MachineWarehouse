using AutoMapper;
using MachineWarehouse.Models.DtoModels;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.View;

namespace MachineWarehouse.Controllers.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(userDto => userDto.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(userDto => userDto.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(userDto => userDto.Name))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(userDto => userDto.Login))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(userDto => userDto.Password))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(userDto => userDto.RoleId));

            CreateMap<User, UserVm>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(user => user.RoleId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom((user) => user.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(((user) => user.Name)))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(user => user.Password))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(user => user.Login))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(user => user.Role.Name));

            CreateMap<UserVm, UserDto>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(userVm => userVm.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(userVm => userVm.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(userVm => userVm.Name))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(userVm => userVm.Login))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(userVm => userVm.Password))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(userVm => userVm.RoleId));
        }
    }
}
