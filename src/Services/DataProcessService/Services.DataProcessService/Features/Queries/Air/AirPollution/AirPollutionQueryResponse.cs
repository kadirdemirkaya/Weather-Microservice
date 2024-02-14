using Services.DataProcessService.Aggregate.Air;
using Services.DataProcessService.Models.Air;

namespace Services.DataProcessService.Features.Queries.Air.AirPollution
{
    public record AirPollutionQueryResponse (
        AirPollutionModel AirPollutionModel    
    );
}
