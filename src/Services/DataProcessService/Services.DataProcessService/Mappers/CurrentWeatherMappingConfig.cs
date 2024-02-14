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

            CreateMap<Aggregate.Current.ValueObjects.Coord, Models.Coord>().ReverseMap();

            CreateMap<Aggregate.Current.ValueObjects.Main, Models.Main>().ReverseMap();

            CreateMap<Aggregate.Current.ValueObjects.Wind, Models.Wind>().ReverseMap();

            CreateMap<Aggregate.Current.ValueObjects.Rain, Models.Rain>().ReverseMap();

            CreateMap<Aggregate.Current.ValueObjects.Cloud, Models.Cloud>().ReverseMap();

            CreateMap<Aggregate.Current.ValueObjects.Sys, Models.Sys>().ReverseMap();
        }
    }
}
