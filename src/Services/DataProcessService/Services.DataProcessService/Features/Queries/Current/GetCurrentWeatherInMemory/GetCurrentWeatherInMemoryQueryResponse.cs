using Services.DataProcessService.Models;

namespace Services.DataProcessService.Features.Queries.Current.GetAirPollutionInMemory
{
    public record GetCurrentWeatherInMemoryQueryResponse (
        CurrentWeatherModel? CurrentWeatherModel
    );
}
