using MediatR;

namespace Services.LocationService.Features.Queries.Location.AirPollution
{
    public record AirPollutionQueryRequest (
        string city
    ) : IRequest<AirPollutionQueryResponse>;
}
