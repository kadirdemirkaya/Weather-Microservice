using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.DataProcessService.Abstractions;
using Services.DataProcessService.Aggregate.Daily;
using Services.DataProcessService.Data;
using Services.DataProcessService.Features.Commands.Daily;
using Services.DataProcessService.Features.Queries.Daily.GetDailyWeatherInMemmory;
using Services.DataProcessService.Models.Daily;

namespace Services.DataProcessService.Services
{
    public class DailyWeatherService : IDailyWeatherService
    {
        private readonly WeatherDbContext _context;
        private readonly IMediator _mediator;

        public DailyWeatherService(WeatherDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<DailyWeatherModel> GetDWeatherModelAsync(Models.Coord coord)
        {
            GetDailyWeatherInMemmoryQueryRequest getDailyRequest = new(coord);
            GetDailyWeatherInMemmoryQueryResponse getDailyResponse = await _mediator.Send(getDailyRequest);
            if (getDailyResponse.DailyWeatherModel is not null)
                return getDailyResponse.DailyWeatherModel;

            DailyWeatherModel? dailyWeatherModel = await _context.Set<DailyWeather>()
                .Where(d => d.City.Lat == coord.lat && d.City.Lon == coord.lon)
                .Include(d => d.DLists)
                .ThenInclude(d => d.Dweather)
                .Select(d => new DailyWeatherModel
                {
                    City = new Models.Daily.City()
                    {
                        country = d.City.Country,
                        id = d.City.id,
                        name = d.City.Name,
                        population = d.City.Population,
                        sunrise = d.City.Sunrise,
                        sunset = d.City.Sunset,
                        timezone = d.City.Timezone,
                        coord = new Models.Coord()
                        {
                            lat = d.City.Lat,
                            lon = d.City.Lon
                        }
                    },
                    DListModels = d.DLists.Select(l => new DListModel
                    {
                        Cloud = new Models.Cloud()
                        {
                            all = l.Clouds.All
                        },
                        Dt = l.Dt,
                        Rain = new Rain()
                        {
                            _3h = l.Rain._3h ?? 0
                        },
                        Main = new Models.Daily.Main()
                        {
                            feels_like = l.Main.Feels_like,
                            grnd_level = l.Main.Grnd_level,
                            humidity = l.Main.Humidity,
                            pressure = l.Main.Pressure,
                            sea_level = l.Main.Sea_level,
                            temp = l.Main.Temp,
                            temp_kf = l.Main.Temp_kf,
                            temp_max = l.Main.Temp_max,
                            temp_min = l.Main.Temp_min
                        },
                        DWeatherModels = l.Dweather.Select(w => new DWeatherModel
                        {
                            Icon = w.Icon,
                            Description = w.Description,
                            id = w.id,
                            Main = w.Main
                        }).ToList()
                    }).ToList()
                }).FirstOrDefaultAsync();

            CreateDailyWeatherInMemoryCommandRequest createDailyRequest = new(coord, dailyWeatherModel);
            CreateDailyWeatherInMemoryCommandResponse createDailyResponse = await _mediator.Send(createDailyRequest);

            return createDailyResponse.response is true ? dailyWeatherModel : default;
        }
    }
}
