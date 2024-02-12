using MediatR;

namespace Services.ClientAndServerService.Features.Weather.Queries.DailyWeather
{
    public class DailyWeatherQueryHandler : IRequestHandler<DailyWeatherQueryRequest, DailyWeatherQueryResponse>
    {
        public Task<DailyWeatherQueryResponse> Handle(DailyWeatherQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
