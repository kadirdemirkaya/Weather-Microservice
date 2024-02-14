using MediatR;

namespace Services.DataProcessService.Features.Queries.Current.CurrentWeather
{
    public record CurrentWeatherQueryRequest(
        double lat,
        double lon
    ) : IRequest<CurrentWeatherQueryResponse>;
}
