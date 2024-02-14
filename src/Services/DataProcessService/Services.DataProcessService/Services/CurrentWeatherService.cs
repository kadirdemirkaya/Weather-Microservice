using Microsoft.EntityFrameworkCore;
using Services.DataProcessService.Abstractions;
using Services.DataProcessService.Data;
using Services.DataProcessService.Models;

namespace Services.DataProcessService.Services
{
    public class CurrentWeatherService : ICurrentWeatherService
    {
        private readonly WeatherDbContext _context;

        public CurrentWeatherService(WeatherDbContext context)
        {
            _context = context;
        }

        public async Task<CurrentWeatherModel> GetCurrentWeatherModelAsync(Coord coord)
        {
            var data = await _context.Set<Aggregate.CurrentWeather>()
                .Where(c => c.Coord.Lat == coord.lat && c.Coord.Lon == coord.lon)
                .Include(c => c.CWeathers)
                .Select(a => new CurrentWeatherModel
                {
                    @base= a.Base,
                    dt = a.Dt,
                    Cloud = new Cloud()
                    {
                        all = a.Cloud.Aal
                    },
                    Rain = new Rain()
                    {
                        _1h = a.Rain._1h
                    },
                    Sys = new Sys()
                    {
                        country = a.Sys.Country,
                        id = a.Sys.id,
                        sunrise = a.Sys.Sunrise,
                        type = a.Sys.Type
                    },
                    Weathers = a.CWeathers.Select(c => new Models.Weather
                    {
                        description = c.Description,
                        icon = c.Icon,
                        id = c.id,
                        main = c.Main
                    }).ToList()
                }).FirstOrDefaultAsync();
            return data;
        }
    }
}
