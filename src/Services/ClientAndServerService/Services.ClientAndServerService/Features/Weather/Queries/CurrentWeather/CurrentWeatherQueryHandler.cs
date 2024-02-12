using MediatR;

namespace Services.ClientAndServerService.Features.Weather.Queries.CurrentWeather
{
    public class CurrentWeatherQueryHandler : IRequestHandler<CurrentWeatherQueryRequest, CurrentWeatherQueryResponse>
    {
        public Task<CurrentWeatherQueryResponse> Handle(CurrentWeatherQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
