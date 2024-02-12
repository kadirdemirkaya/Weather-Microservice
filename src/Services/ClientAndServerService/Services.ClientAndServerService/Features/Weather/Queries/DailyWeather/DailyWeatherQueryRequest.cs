using MediatR;

namespace Services.ClientAndServerService.Features.Weather.Queries.DailyWeather
{
    public record DailyWeatherQueryRequest(

    ) : IRequest<DailyWeatherQueryResponse>;
}
