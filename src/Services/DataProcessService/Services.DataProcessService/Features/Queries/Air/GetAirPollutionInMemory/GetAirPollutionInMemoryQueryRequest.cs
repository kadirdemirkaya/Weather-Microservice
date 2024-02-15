using MediatR;

namespace Services.DataProcessService.Features.Queries.Air.AirPollutionInMemory
{
    public record GetAirPollutionInMemoryQueryRequest (
        Models.Air.Coord coord
    ) : IRequest<GetAirPollutionInMemoryQueryResponse>;
}
