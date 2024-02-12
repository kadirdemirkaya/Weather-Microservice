using AutoMapper;
using Services.DataProcessService.Aggregate;
using Services.DataProcessService.Models;

namespace Services.DataProcessService.Mappers
{
    public class CurrentWeatherMappingConfig : Profile
    {
        public CurrentWeatherMappingConfig()
        {
            CreateMap<WeatherData, CurrentWeather>().ReverseMap();

            CreateMap<Services.DataProcessService.Aggregate.Current.ValueObjects.Coord, Services.DataProcessService.Models.Coord>().ReverseMap();

            CreateMap<Services.DataProcessService.Aggregate.Current.ValueObjects.Main, Services.DataProcessService.Models.Main>().ReverseMap();

            CreateMap<Services.DataProcessService.Aggregate.Current.ValueObjects.Wind, Services.DataProcessService.Models.Wind>().ReverseMap();

            CreateMap<Services.DataProcessService.Aggregate.Current.ValueObjects.Rain, Services.DataProcessService.Models.Rain>().ReverseMap();

            CreateMap<Services.DataProcessService.Aggregate.Current.ValueObjects.Cloud, Services.DataProcessService.Models.Cloud>().ReverseMap();

            CreateMap<Services.DataProcessService.Aggregate.Current.ValueObjects.Sys, Services.DataProcessService.Models.Sys>().ReverseMap();
        }
    }
}
