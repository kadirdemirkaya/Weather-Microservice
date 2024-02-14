using MediatR;

namespace Services.DataProcessService.Features.Queries.Air.AirPollution
{
    public record AirPollutionQueryRequest(
        double lat,
        double lon
    ) : IRequest<AirPollutionQueryResponse>;
}
