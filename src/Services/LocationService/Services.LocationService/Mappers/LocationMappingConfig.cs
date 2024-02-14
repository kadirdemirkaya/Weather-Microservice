using AutoMapper;
using LocationService;

namespace Services.LocationService.Mappers
{
    public class LocationMappingConfig : Profile
    {
        public LocationMappingConfig()
        {
            //Current
            CreateMap<CurrentWeatherModel, DataCaptureService.CurrentWeatherModel>()
                .ForMember(dest => dest.Base, opt => opt.MapFrom(src => src.Base)).ReverseMap()
                .ForMember(dest => dest.CurrentCloud, opt => opt.MapFrom(src => src.CurrentCloud)).ReverseMap()
                .ForMember(dest => dest.CurrentRain, opt => opt.MapFrom(src => src.CurrentRain)).ReverseMap()
                .ForMember(dest => dest.CurrentSys, opt => opt.MapFrom(src => src.CurrentSys)).ReverseMap()
                .ForMember(dest => dest.CurrentWeather, opt => opt.MapFrom(src => src.CurrentWeather)).ReverseMap()
                .ForMember(dest => dest.Dt, opt => opt.MapFrom(src => src.Dt)).ReverseMap();

            CreateMap<CurrentRain, DataCaptureService.CurrentRain>()
                .ForMember(dest => dest._1H, opt => opt.MapFrom(src => src._1H)).ReverseMap();

            CreateMap<CurrentCloud, DataCaptureService.CurrentCloud>()
                .ForMember(dest => dest.All, opt => opt.MapFrom(src => src.All)).ReverseMap();

            CreateMap<CurrentSys, DataCaptureService.CurrentSys>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country)).ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap()
                .ForMember(dest => dest.Sunrise, opt => opt.MapFrom(src => src.Sunrise)).ReverseMap()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type)).ReverseMap();

            CreateMap<CurrentWeather, DataCaptureService.CurrentWeather>()
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon)).ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap()
                .ForMember(dest => dest.Main, opt => opt.MapFrom(src => src.Main)).ReverseMap()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description)).ReverseMap();

            //Air
            CreateMap<AirPollutionModel, DataCaptureService.AirPollutionModel>()
             .ForMember(dest => dest.AirListModel, opt => opt.MapFrom(src => src.AirListModel)).ReverseMap();

            CreateMap<AirListModel, DataCaptureService.AirListModel>()
             .ForMember(dest => dest.AirComponent, opt => opt.MapFrom(src => src.AirComponent)).ReverseMap()
             .ForMember(dest => dest.Dt, opt => opt.MapFrom(src => src.Dt)).ReverseMap()
             .ForMember(dest => dest.Main, opt => opt.MapFrom(src => src.Main)).ReverseMap();

            CreateMap<AirMain, DataCaptureService.AirMain>()
             .ForMember(dest => dest.Aqi, opt => opt.MapFrom(src => src.Aqi)).ReverseMap();

            CreateMap<AirComponent, DataCaptureService.AirComponent>()
             .ForMember(dest => dest.Co, opt => opt.MapFrom(src => src.Co)).ReverseMap()
             .ForMember(dest => dest.Nh3, opt => opt.MapFrom(src => src.Nh3)).ReverseMap()
             .ForMember(dest => dest.No, opt => opt.MapFrom(src => src.No)).ReverseMap()
             .ForMember(dest => dest.No2, opt => opt.MapFrom(src => src.No2)).ReverseMap()
             .ForMember(dest => dest.O3, opt => opt.MapFrom(src => src.O3)).ReverseMap()
             .ForMember(dest => dest.Pm10, opt => opt.MapFrom(src => src.Pm10)).ReverseMap()
             .ForMember(dest => dest.Pm2, opt => opt.MapFrom(src => src.Pm2)).ReverseMap()
             .ForMember(dest => dest.So2, opt => opt.MapFrom(src => src.So2)).ReverseMap();
        }
    }
}
