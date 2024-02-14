using Microsoft.EntityFrameworkCore;
using Services.DataProcessService.Abstractions;
using Services.DataProcessService.Aggregate.Air;
using Services.DataProcessService.Data;
using Services.DataProcessService.Models.Air;

namespace Services.DataProcessService.Services
{
    public class AirPollutionService : IAirPollutionService
    {
        private readonly WeatherDbContext _context;

        public AirPollutionService(WeatherDbContext context)
        {
            _context = context;
        }

        public async Task<AirPollutionModel> GetAirPollutionAsync(Models.Air.Coord coord)
        {
            return await _context.Set<AirPollutionWeather>()
                   .Where(a => a.Coord.Latitude == coord.lat && a.Coord.Longitude == coord.lon)
                   .Include(a => a.ALists)
                   .Select(a => new AirPollutionModel
                   {
                       AirListModels = a.ALists.Select(al => new AirListModel
                       {
                           Component = new()
                           {
                               co = al.Components.Co,
                               nh3 = al.Components.Nh3,
                               no = al.Components.No,
                               no2 = al.Components.No2,
                               o3 = al.Components.O3,
                               pm10 = al.Components.Pm10,
                               pm2 = al.Components.Pm2,
                               so2 = al.Components.So2,
                           },
                           Dt = al.Dt,
                           Main = new()
                           {
                               aqi = al.Main.Aqi
                           }
                       }).ToList()
                   })
                   .FirstOrDefaultAsync();
        }
    }
}
