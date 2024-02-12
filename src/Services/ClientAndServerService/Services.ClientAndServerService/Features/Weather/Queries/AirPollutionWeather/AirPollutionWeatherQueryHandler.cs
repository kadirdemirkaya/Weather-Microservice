using MediatR;

namespace Services.ClientAndServerService.Features.Weather.Queries.AirPollutionWeather
{
    public class AirPollutionWeatherQueryHandler : IRequestHandler<AirPollutionWeatherQueryRequest, AirPollutionWeatherQueryResponse>
    {
        public Task<AirPollutionWeatherQueryResponse> Handle(AirPollutionWeatherQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
