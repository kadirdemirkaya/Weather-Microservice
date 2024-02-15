using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.DataProcessService.Abstractions;
using Services.DataProcessService.Aggregate.Air;
using Services.DataProcessService.Data;
using Services.DataProcessService.Features.Commands.Air.GetAirPollutionInMemory;
using Services.DataProcessService.Features.Queries.Air.AirPollutionInMemory;
using Services.DataProcessService.Models.Air;

namespace Services.DataProcessService.Services
{
    public class AirPollutionService : IAirPollutionService
    {
        private readonly WeatherDbContext _context;
        private readonly IMediator _mediator;
        public AirPollutionService(WeatherDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<AirPollutionModel> GetAirPollutionAsync(Models.Air.Coord coord)
        {
            GetAirPollutionInMemoryQueryRequest getAirRequest = new(coord);
            GetAirPollutionInMemoryQueryResponse getAirResponse = await _mediator.Send(getAirRequest);
            if (getAirResponse.AirPollutionModel is not null)
                return getAirResponse.AirPollutionModel;

            AirPollutionModel? airPollutionModel = await _context.Set<AirPollutionWeather>()
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

            CreateAirPollutionInMemoryCommandRequest createAirRequest = new(coord, airPollutionModel);
            CreateAirPollutionInMemoryCommandResponse createAirResponse = await _mediator.Send(createAirRequest);

            return createAirResponse.response is true ? airPollutionModel : default;
        }
    }
}
