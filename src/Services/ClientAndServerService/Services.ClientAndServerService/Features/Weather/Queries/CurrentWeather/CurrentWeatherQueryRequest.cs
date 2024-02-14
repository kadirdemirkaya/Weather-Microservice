using MediatR;

namespace Services.ClientAndServerService.Features.Weather.Queries.CurrentWeather
{
    public record CurrentWeatherQueryRequest(
        string City
    ) : IRequest<CurrentWeatherQueryResponse>;
}
