using MediatR;

namespace Services.ClientAndServerService.Features.Weather.Queries.AirPollutionWeather
{
    public record AirPollutionWeatherQueryRequest (
    
    ) : IRequest<AirPollutionWeatherQueryResponse>;
}
