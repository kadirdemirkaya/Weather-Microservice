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

            //Air
            CreateMap<LocationService.AirPollutionModel, Models.AirPollutionModel>()
            .ForMember(dest => dest.AirListModels, opt => opt.MapFrom(src => src.AirListModel)).ReverseMap();

            CreateMap<LocationService.AirListModel, Models.AirListModel>()
                .ForMember(dest => dest.Component, opt => opt.MapFrom(src => src.AirComponent)).ReverseMap()
                .ForMember(dest => dest.Dt, opt => opt.MapFrom(src => src.Dt)).ReverseMap()
                .ForMember(dest => dest.Main, opt => opt.MapFrom(src => src.Main)).ReverseMap();

            CreateMap<LocationService.AirMain, Models.Air.Main>()
                .ForMember(dest => dest.aqi, opt => opt.MapFrom(src => src.Aqi)).ReverseMap();

            CreateMap<LocationService.AirComponent, Models.Air.Component>()
                .ForMember(dest => dest.co, opt => opt.MapFrom(src => src.Co)).ReverseMap()
                .ForMember(dest => dest.Nh3, opt => opt.MapFrom(src => src.nh3)).ReverseMap()
                .ForMember(dest => dest.no, opt => opt.MapFrom(src => src.No)).ReverseMap()
                .ForMember(dest => dest.No2, opt => opt.MapFrom(src => src.no2)).ReverseMap()
                .ForMember(dest => dest.o3, opt => opt.MapFrom(src => src.O3)).ReverseMap()
                .ForMember(dest => dest.Pm10, opt => opt.MapFrom(src => src.pm10)).ReverseMap()
                .ForMember(dest => dest.pm2, opt => opt.MapFrom(src => src.Pm2)).ReverseMap()
                .ForMember(dest => dest.So2, opt => opt.MapFrom(src => src.so2)).ReverseMap();
        }
    }
}