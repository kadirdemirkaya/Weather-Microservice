using MediatR;

namespace Services.DataProcessService.Features.Queries.Current.GetAirPollutionInMemory
{
    public record GetCurrentWeatherInMemoryQueryRequest(
        Models.Coord coord
    ) : IRequest<GetCurrentWeatherInMemoryQueryResponse>;
}
