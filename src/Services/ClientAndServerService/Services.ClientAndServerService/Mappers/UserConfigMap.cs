using AutoMapper;
using Services.ClientAndServerService.Dtos;
using Services.ClientAndServerService.Models;

namespace Services.ClientAndServerService.Mappers
{
    public class UserConfigMap : Profile
    {
        public UserConfigMap()
        {
            CreateMap<UserLoginModel, UserLoginModelDto>().ReverseMap();
            CreateMap<UserRegisterModel, UserRegisterModelDto>().ReverseMap();
        }
    }
}
