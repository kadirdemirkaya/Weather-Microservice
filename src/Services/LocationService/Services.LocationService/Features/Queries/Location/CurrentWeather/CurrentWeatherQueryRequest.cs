using MediatR;

namespace Services.LocationService.Features.Queries.Location.CurrentWeather
{
    public record CurrentWeatherQueryRequest (
        string city
    ) : IRequest<CurrentWeatherQueryResponse>;
}
