using Services.DataProcessService.Models.Air;

namespace Services.DataProcessService.Features.Queries.Air.AirPollutionInMemory
{
    public record GetAirPollutionInMemoryQueryResponse (
        AirPollutionModel? AirPollutionModel
    );
}
