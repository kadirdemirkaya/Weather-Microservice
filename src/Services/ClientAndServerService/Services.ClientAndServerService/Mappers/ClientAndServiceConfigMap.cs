using AutoMapper;

namespace Services.ClientAndServerService.Mappers
{
    public class ClientAndServiceConfigMap : Profile
    {
        public ClientAndServiceConfigMap()
        {
            CreateMap<LocationService.CurrentWeatherModel, Models.CurrentWeatherModel>()
              .ForMember(dest => dest.Base, opt => opt.MapFrom(src => src.Base)).ReverseMap()
              .ForMember(dest => dest.CurrentCloud, opt => opt.MapFrom(src => src.Cloud)).ReverseMap()
              .ForMember(dest => dest.Dt, opt => opt.MapFrom(src => src.Dt)).ReverseMap()
              .ForMember(dest => dest.CurrentRain, opt => opt.MapFrom(src => src.Rain)).ReverseMap()
              .ForMember(dest => dest.Sys, opt => opt.MapFrom(src => src.CurrentSys)).ReverseMap()
              .ForMember(dest => dest.CurrentWeather, opt => opt.MapFrom(src => src.Weathers)).ReverseMap();

            CreateMap<LocationService.CurrentRain, Models.Current.Rain>()
              .ForMember(dest => dest._1h, opt => opt.MapFrom(src => src._1H)).ReverseMap();

            CreateMap<LocationService.CurrentCloud, Models.Current.Cloud>()
              .ForMember(dest => dest.all, opt => opt.MapFrom(src => src.All)).ReverseMap();

            CreateMap<LocationService.CurrentSys, Models.Current.Sys>()
              .ForMember(dest => dest.country, opt => opt.MapFrom(src => src.Country)).ReverseMap()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id)).ReverseMap()
              .ForMember(dest => dest.sunrise, opt => opt.MapFrom(src => src.Sunrise)).ReverseMap()
              .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.type)).ReverseMap();

            CreateMap<LocationService.CurrentWeather, Models.Current.Weather>()
              .ForMember(dest => dest.icon, opt => opt.MapFrom(src => src.Icon)).ReverseMap()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id)).ReverseMap()
              .ForMember(dest => dest.main, opt => opt.MapFrom(src => src.Main)).ReverseMap()
              .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description)).ReverseMap();
        }
    }
}