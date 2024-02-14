using Services.DataProcessService.Models.Air;

namespace Services.DataProcessService.Abstractions
{
    public interface IAirPollutionService
    {
        Task<AirPollutionModel> GetAirPollutionAsync(Coord coord);
    }
}
