using MediatR;

namespace Services.LocationService.Features.Queries.Location.DailyWeather
{
    public record DailyWeatherQueryRequest(
        string city
    ) : IRequest<DailyWeatherQueryResponse>;
}
