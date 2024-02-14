using AutoMapper;
using DataProcessService;

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

            //Air
            CreateMap<Models.Air.AirPollutionModel, AirPollutionModel>()
                .ForMember(dest => dest.AirListModel, opt => opt.MapFrom(src => src.AirListModels)).ReverseMap();

            CreateMap<Models.Air.AirListModel, AirListModel>()
                .ForMember(dest => dest.AirComponent, opt => opt.MapFrom(src => src.Component)).ReverseMap()
                .ForMember(dest => dest.Dt, opt => opt.MapFrom(src => src.Dt)).ReverseMap()
                .ForMember(dest => dest.Main, opt => opt.MapFrom(src => src.Main)).ReverseMap();

            CreateMap<Models.Air.Main, AirMain>()
                .ForMember(dest => dest.Aqi, opt => opt.MapFrom(src => src.aqi)).ReverseMap();

            CreateMap<Models.Air.Component, AirComponent>()
                .ForMember(dest => dest.Co, opt => opt.MapFrom(src => src.co)).ReverseMap()
                .ForMember(dest => dest.nh3, opt => opt.MapFrom(src => src.Nh3)).ReverseMap()
                .ForMember(dest => dest.No, opt => opt.MapFrom(src => src.no)).ReverseMap()
                .ForMember(dest => dest.no2, opt => opt.MapFrom(src => src.No2)).ReverseMap()
                .ForMember(dest => dest.O3, opt => opt.MapFrom(src => src.o3)).ReverseMap()
                .ForMember(dest => dest.pm10, opt => opt.MapFrom(src => src.Pm10)).ReverseMap()
                .ForMember(dest => dest.Pm2, opt => opt.MapFrom(src => src.pm2)).ReverseMap()
                .ForMember(dest => dest.so2, opt => opt.MapFrom(src => src.So2)).ReverseMap();

            //Daily
            CreateMap<Models.Daily.DailyWeatherModel, DailyWeatherDataModel>()
                .ForMember(dest => dest.DailyCity, opt => opt.MapFrom(src => src.City)).ReverseMap()
                .ForMember(dest => dest.DListModels, opt => opt.MapFrom(src => src.DailyListModel)).ReverseMap();

            CreateMap<Models.Daily.City, DailyCity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id)).ReverseMap()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name)).ReverseMap()
                .ForMember(dest => dest.Population, opt => opt.MapFrom(src => src.population)).ReverseMap()
                .ForMember(dest => dest.sunrise, opt => opt.MapFrom(src => src.Sunrise)).ReverseMap()
                .ForMember(dest => dest.Sunset, opt => opt.MapFrom(src => src.sunset)).ReverseMap()
                .ForMember(dest => dest.timezone, opt => opt.MapFrom(src => src.Timezone)).ReverseMap()
                .ForMember(dest => dest.DailyCoord, opt => opt.MapFrom(src => src.coord)).ReverseMap();

            CreateMap<Models.Coord, DailyCoord>()
                .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.lat)).ReverseMap()
                .ForMember(dest => dest.lon, opt => opt.MapFrom(src => src.Lon)).ReverseMap();

            CreateMap<Models.Daily.DListModel, DailyListModel>()
                .ForMember(dest => dest.DailyCloud, opt => opt.MapFrom(src => src.Cloud)).ReverseMap()
                .ForMember(dest => dest.Dt, opt => opt.MapFrom(src => src.Dt)).ReverseMap()
                .ForMember(dest => dest.DailyWeatherModel, opt => opt.MapFrom(src => src.DWeatherModels)).ReverseMap()
                .ForMember(dest => dest.Main, opt => opt.MapFrom(src => src.DailyMain)).ReverseMap()
                .ForMember(dest => dest.DailyRain, opt => opt.MapFrom(src => src.Rain)).ReverseMap()
                .ForMember(dest => dest.Cloud, opt => opt.MapFrom(src => src.DailyCloud)).ReverseMap();

            CreateMap<Models.Daily.Main, DailyMain>()
                .ForMember(dest => dest.Feelslike, opt => opt.MapFrom(src => src.feels_like)).ReverseMap()
                .ForMember(dest => dest.grnd_level, opt => opt.MapFrom(src => src.Grndlevel)).ReverseMap()
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.humidity)).ReverseMap()
                .ForMember(dest => dest.pressure, opt => opt.MapFrom(src => src.Pressure)).ReverseMap()
                .ForMember(dest => dest.Sealevel, opt => opt.MapFrom(src => src.sea_level)).ReverseMap()
                .ForMember(dest => dest.temp, opt => opt.MapFrom(src => src.Temp)).ReverseMap()
                .ForMember(dest => dest.Tempmax, opt => opt.MapFrom(src => src.temp_max)).ReverseMap()
                .ForMember(dest => dest.temp_min, opt => opt.MapFrom(src => src.Tempmin)).ReverseMap()
                .ForMember(dest => dest.Tempkf, opt => opt.MapFrom(src => src.temp_kf)).ReverseMap();

            CreateMap<Models.Cloud, DailyCloud>()
                .ForMember(dest => dest.All, opt => opt.MapFrom(src => src.all)).ReverseMap();

            CreateMap<Models.Daily.Rain, DailyRain>()
                .ForMember(dest => dest._3H, opt => opt.MapFrom(src => src._3h)).ReverseMap();

            CreateMap<Models.Daily.DWeatherModel, DailyWeatherModel>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description)).ReverseMap()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id)).ReverseMap()
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon)).ReverseMap()
                .ForMember(dest => dest.Main, opt => opt.MapFrom(src => src.Main)).ReverseMap();
        }
    }
}
