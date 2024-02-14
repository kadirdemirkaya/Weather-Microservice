using AutoMapper;
using DataCaptureService;

namespace Services.DataProcessService.Mappers
{
    public class WeatherMappingConfig : Profile
    {
        public WeatherMappingConfig()
        {
            //Current
            CreateMap<Models.CurrentWeatherModel, CurrentWeatherModel>()
                .ForMember(dest => dest.Base, opt => opt.MapFrom(src => src.@base)).ReverseMap()
                .ForMember(dest => dest.Cloud, opt => opt.MapFrom(src => src.CurrentCloud)).ReverseMap()
                .ForMember(dest => dest.Dt, opt => opt.MapFrom(src => src.dt)).ReverseMap()
                .ForMember(dest => dest.Rain, opt => opt.MapFrom(src => src.CurrentRain)).ReverseMap()
                .ForMember(dest => dest.CurrentSys, opt => opt.MapFrom(src => src.Sys)).ReverseMap()
                .ForMember(dest => dest.Weathers, opt => opt.MapFrom(src => src.CurrentWeather)).ReverseMap();

            CreateMap<Models.Rain, CurrentRain>()
                .ForMember(dest => dest._1H, opt => opt.MapFrom(src => src._1h)).ReverseMap();

            CreateMap<Models.Cloud, CurrentCloud>()
                .ForMember(dest => dest.All, opt => opt.MapFrom(src => src.all)).ReverseMap();

            CreateMap<Models.Sys, CurrentSys>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.country)).ReverseMap()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id)).ReverseMap()
                .ForMember(dest => dest.Sunrise, opt => opt.MapFrom(src => src.sunrise)).ReverseMap()
                .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.Type)).ReverseMap();

            CreateMap<Models.Weather, CurrentWeather>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description)).ReverseMap()
                .ForMember(dest => dest.icon, opt => opt.MapFrom(src => src.Icon)).ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id)).ReverseMap()
                .ForMember(dest => dest.main, opt => opt.MapFrom(src => src.Main)).ReverseMap();

        }
    }
}
