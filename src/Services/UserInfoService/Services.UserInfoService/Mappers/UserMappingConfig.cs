using AutoMapper;
using Services.UserInfoService.Aggregates;
using Services.UserInfoService.Dtos;
using UserInfoService;

namespace Services.UserInfoService.Mappers
{
    public class UserMappingConfig : Profile
    {
        public UserMappingConfig()
        {
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<UserLoginModel, UserLoginDto>().ReverseMap();
            CreateMap<UserRegisterModel, UserRegisterDto>().ReverseMap();

        }
    }
}
