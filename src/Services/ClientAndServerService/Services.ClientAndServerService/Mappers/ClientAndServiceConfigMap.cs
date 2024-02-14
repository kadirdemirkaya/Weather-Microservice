using AutoMapper;

namespace Services.ClientAndServerService.Mappers
{
    public class ClientAndServiceConfigMap : Profile
    {
        public ClientAndServiceConfigMap()
        {
            //Current
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

            //Daily
            CreateMap<LocationService.DailyWeatherDataModel, Models.DailyWeatherDataModel>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.DailyCity)).ReverseMap()
            .ForMember(dest => dest.DailyListModel, opt => opt.MapFrom(src => src.DailyListModel)).ReverseMap();

            CreateMap<LocationService.DailyCity, Models.Daily.City>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id)).ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name)).ReverseMap()
                .ForMember(dest => dest.population, opt => opt.MapFrom(src => src.Population)).ReverseMap()
                .ForMember(dest => dest.Sunrise, opt => opt.MapFrom(src => src.sunrise)).ReverseMap()
                .ForMember(dest => dest.sunset, opt => opt.MapFrom(src => src.Sunset)).ReverseMap()
                .ForMember(dest => dest.Timezone, opt => opt.MapFrom(src => src.timezone)).ReverseMap()
                .ForMember(dest => dest.coord, opt => opt.MapFrom(src => src.DailyCoord)).ReverseMap();

            CreateMap<LocationService.DailyCoord, Models.Daily.Coord>()
                .ForMember(dest => dest.lat, opt => opt.MapFrom(src => src.Lat)).ReverseMap()
                .ForMember(dest => dest.Lon, opt => opt.MapFrom(src => src.lon)).ReverseMap();

            CreateMap<LocationService.DailyListModel, Models.DailyListModel>()
                .ForMember(dest => dest.Cloud, opt => opt.MapFrom(src => src.DailyCloud)).ReverseMap()
                .ForMember(dest => dest.Dt, opt => opt.MapFrom(src => src.Dt)).ReverseMap()
                .ForMember(dest => dest.DWeatherModels, opt => opt.MapFrom(src => src.DailyWeatherModel)).ReverseMap()
                .ForMember(dest => dest.DailyMain, opt => opt.MapFrom(src => src.Main)).ReverseMap()
                .ForMember(dest => dest.Rain, opt => opt.MapFrom(src => src.DailyRain)).ReverseMap();

            CreateMap<LocationService.DailyMain, Models.Daily.Main>()
                .ForMember(dest => dest.feels_like, opt => opt.MapFrom(src => src.Feelslike)).ReverseMap()
                .ForMember(dest => dest.Grndlevel, opt => opt.MapFrom(src => src.grnd_level)).ReverseMap()
                .ForMember(dest => dest.humidity, opt => opt.MapFrom(src => src.Humidity)).ReverseMap()
                .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.pressure)).ReverseMap()
                .ForMember(dest => dest.sea_level, opt => opt.MapFrom(src => src.Sealevel)).ReverseMap()
                .ForMember(dest => dest.Temp, opt => opt.MapFrom(src => src.temp)).ReverseMap()
                .ForMember(dest => dest.temp_max, opt => opt.MapFrom(src => src.Tempmax)).ReverseMap()
                .ForMember(dest => dest.Tempmin, opt => opt.MapFrom(src => src.temp_min)).ReverseMap()
                .ForMember(dest => dest.temp_kf, opt => opt.MapFrom(src => src.Tempkf)).ReverseMap();

            CreateMap<LocationService.DailyCloud, Models.Daily.Cloud>()
                .ForMember(dest => dest.all, opt => opt.MapFrom(src => src.All)).ReverseMap();

            CreateMap<LocationService.DailyRain, Models.Daily.Rain>()
                .ForMember(dest => dest._3h, opt => opt.MapFrom(src => src._3H)).ReverseMap();

            CreateMap<LocationService.DailyWeatherModel, Models.DWeatherModel>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description)).ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id)).ReverseMap()
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon)).ReverseMap()
                .ForMember(dest => dest.Main, opt => opt.MapFrom(src => src.Main)).ReverseMap();
        }
    }
}