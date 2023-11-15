using AutoMapper;
using MachineWarehouse.Controllers.DtoModels.UserModels;
using MachineWarehouse.Models.Entities;
using MachineWarehouse.Models.Request.UserRequestModels;
using MachineWarehouse.Models.Request.UserVm;

namespace MachineWarehouse.Controllers.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<CreateUserDto, CreateUser>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(userDto => userDto.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(userDto => userDto.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(userDto => userDto.Password))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(userDto => userDto.Login))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(userDto => userDto.RoleName));

            CreateMap<UpdateUserDto, UpdateUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(userDto => userDto.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(userDto => userDto.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(userDto => userDto.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(userDto => userDto.Password))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(userDto => userDto.Login))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(userDto => userDto.RoleName));

            CreateMap<User, GetUserVm>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(userDto => userDto.RoleId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom((userDto) => userDto.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(((userDto) => userDto.Name)));

            CreateMap<User, GetUsersVm>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(userDto => userDto.RoleId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom((userDto) => userDto.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(((userDto) => userDto.Name)));


        }
    }
}
