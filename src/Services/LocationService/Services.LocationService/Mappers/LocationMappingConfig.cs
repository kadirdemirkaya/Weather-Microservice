using AutoMapper;
using LocationService;

namespace Services.LocationService.Mappers
{
    public class LocationMappingConfig : Profile
    {
        public LocationMappingConfig()
        {
            //Current
            CreateMap<CurrentWeatherModel, DataProcessService.CurrentWeatherModel>()
                .ForMember(dest => dest.Base, opt => opt.MapFrom(src => src.Base)).ReverseMap()
                .ForMember(dest => dest.CurrentCloud, opt => opt.MapFrom(src => src.CurrentCloud)).ReverseMap()
                .ForMember(dest => dest.CurrentRain, opt => opt.MapFrom(src => src.CurrentRain)).ReverseMap()
                .ForMember(dest => dest.CurrentSys, opt => opt.MapFrom(src => src.CurrentSys)).ReverseMap()
                .ForMember(dest => dest.CurrentWeather, opt => opt.MapFrom(src => src.CurrentWeather)).ReverseMap()
                .ForMember(dest => dest.Dt, opt => opt.MapFrom(src => src.Dt)).ReverseMap();

            CreateMap<CurrentRain, DataProcessService.CurrentRain>()
                .ForMember(dest => dest._1H, opt => opt.MapFrom(src => src._1H)).ReverseMap();

            CreateMap<CurrentCloud, DataProcessService.CurrentCloud>()
                .ForMember(dest => dest.All, opt => opt.MapFrom(src => src.All)).ReverseMap();

            CreateMap<CurrentSys, DataProcessService.CurrentSys>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country)).ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap()
                .ForMember(dest => dest.Sunrise, opt => opt.MapFrom(src => src.Sunrise)).ReverseMap()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type)).ReverseMap();

            CreateMap<CurrentWeather, DataProcessService.CurrentWeather>()
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon)).ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap()
                .ForMember(dest => dest.Main, opt => opt.MapFrom(src => src.Main)).ReverseMap()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description)).ReverseMap();

            //Air
            CreateMap<AirPollutionModel, DataProcessService.AirPollutionModel>()
             .ForMember(dest => dest.AirListModel, opt => opt.MapFrom(src => src.AirListModel)).ReverseMap();

            CreateMap<AirListModel, DataProcessService.AirListModel>()
             .ForMember(dest => dest.AirComponent, opt => opt.MapFrom(src => src.AirComponent)).ReverseMap()
             .ForMember(dest => dest.Dt, opt => opt.MapFrom(src => src.Dt)).ReverseMap()
             .ForMember(dest => dest.Main, opt => opt.MapFrom(src => src.Main)).ReverseMap();

            CreateMap<AirMain, DataProcessService.AirMain>()
             .ForMember(dest => dest.Aqi, opt => opt.MapFrom(src => src.Aqi)).ReverseMap();

            CreateMap<AirComponent, DataProcessService.AirComponent>()
             .ForMember(dest => dest.Co, opt => opt.MapFrom(src => src.Co)).ReverseMap()
             .ForMember(dest => dest.Nh3, opt => opt.MapFrom(src => src.Nh3)).ReverseMap()
             .ForMember(dest => dest.No, opt => opt.MapFrom(src => src.No)).ReverseMap()
             .ForMember(dest => dest.No2, opt => opt.MapFrom(src => src.No2)).ReverseMap()
             .ForMember(dest => dest.O3, opt => opt.MapFrom(src => src.O3)).ReverseMap()
             .ForMember(dest => dest.Pm10, opt => opt.MapFrom(src => src.Pm10)).ReverseMap()
             .ForMember(dest => dest.Pm2, opt => opt.MapFrom(src => src.Pm2)).ReverseMap()
             .ForMember(dest => dest.So2, opt => opt.MapFrom(src => src.So2)).ReverseMap();

            //Daily
            CreateMap<DailyWeatherDataModel, DataProcessService.DailyWeatherDataModel>()
             .ForMember(dest => dest.DailyCity, opt => opt.MapFrom(src => src.DailyCity)).ReverseMap()
             .ForMember(dest => dest.DailyListModel, opt => opt.MapFrom(src => src.DailyListModel)).ReverseMap();

            CreateMap<DailyCity, DataProcessService.DailyCity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap()
                .ForMember(dest => dest.Population, opt => opt.MapFrom(src => src.Population)).ReverseMap()
                .ForMember(dest => dest.Sunrise, opt => opt.MapFrom(src => src.Sunrise)).ReverseMap()
                .ForMember(dest => dest.Sunset, opt => opt.MapFrom(src => src.Sunset)).ReverseMap()
                .ForMember(dest => dest.Timezone, opt => opt.MapFrom(src => src.Timezone)).ReverseMap()
                .ForMember(dest => dest.DailyCoord, opt => opt.MapFrom(src => src.DailyCoord)).ReverseMap();

            CreateMap<DailyCoord, DataProcessService.DailyCoord>()
                .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat)).ReverseMap()
                .ForMember(dest => dest.Lon, opt => opt.MapFrom(src => src.Lon)).ReverseMap();

            CreateMap<DailyListModel, DataProcessService.DailyListModel>()
                .ForMember(dest => dest.DailyCloud, opt => opt.MapFrom(src => src.DailyCloud)).ReverseMap()
                .ForMember(dest => dest.Dt, opt => opt.MapFrom(src => src.Dt)).ReverseMap()
                .ForMember(dest => dest.DailyWeatherModel, opt => opt.MapFrom(src => src.DailyWeatherModel)).ReverseMap()
                .ForMember(dest => dest.DailyMain, opt => opt.MapFrom(src => src.DailyMain)).ReverseMap()
                .ForMember(dest => dest.DailyRain, opt => opt.MapFrom(src => src.DailyRain)).ReverseMap()
                .ForMember(dest => dest.DailyCloud, opt => opt.MapFrom(src => src.DailyCloud)).ReverseMap();

            CreateMap<DailyMain, DataProcessService.DailyMain>()
                .ForMember(dest => dest.Feelslike, opt => opt.MapFrom(src => src.Feelslike)).ReverseMap()
                .ForMember(dest => dest.Grndlevel, opt => opt.MapFrom(src => src.Grndlevel)).ReverseMap()
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Humidity)).ReverseMap()
                .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.Pressure)).ReverseMap()
                .ForMember(dest => dest.Sealevel, opt => opt.MapFrom(src => src.Sealevel)).ReverseMap()
                .ForMember(dest => dest.Temp, opt => opt.MapFrom(src => src.Temp)).ReverseMap()
                .ForMember(dest => dest.Tempmax, opt => opt.MapFrom(src => src.Tempmax)).ReverseMap()
                .ForMember(dest => dest.Tempmin, opt => opt.MapFrom(src => src.Tempmin)).ReverseMap()
                .ForMember(dest => dest.Tempkf, opt => opt.MapFrom(src => src.Tempkf)).ReverseMap();

            CreateMap<DailyCloud, DataProcessService.DailyCloud>()
                .ForMember(dest => dest.All, opt => opt.MapFrom(src => src.All)).ReverseMap();

            CreateMap<DailyRain, DataProcessService.DailyRain>()
                .ForMember(dest => dest._3H, opt => opt.MapFrom(src => src._3H)).ReverseMap();

            CreateMap<DailyWeatherModel, DataProcessService.DailyWeatherModel>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description)).ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap()
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon)).ReverseMap()
                .ForMember(dest => dest.Main, opt => opt.MapFrom(src => src.Main)).ReverseMap();
        }
    }
}
