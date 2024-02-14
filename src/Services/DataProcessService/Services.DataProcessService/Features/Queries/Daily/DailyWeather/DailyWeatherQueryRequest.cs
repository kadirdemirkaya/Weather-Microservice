using MediatR;

namespace Services.DataProcessService.Features.Queries.Daily.DailyWeather
{
    public record DailyWeatherQueryRequest (
        double lat,
        double lon
    ) : IRequest<DailyWeatherQueryResponse>;
}
