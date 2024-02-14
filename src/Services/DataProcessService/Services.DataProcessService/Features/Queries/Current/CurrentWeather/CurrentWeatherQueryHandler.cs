using MediatR;
using Services.DataProcessService.Abstractions;

namespace Services.DataProcessService.Features.Queries.Current.CurrentWeather
{
    public class CurrentWeatherQueryHandler : IRequestHandler<CurrentWeatherQueryRequest, CurrentWeatherQueryResponse>
    {
        private readonly ICurrentWeatherService _currentWeatherService;

        public CurrentWeatherQueryHandler(ICurrentWeatherService currentWeatherService)
        {
            _currentWeatherService = currentWeatherService;
        }

        public async Task<CurrentWeatherQueryResponse> Handle(CurrentWeatherQueryRequest request, CancellationToken cancellationToken)
            => new(await _currentWeatherService.GetCurrentWeatherModelAsync(new() { lat = request.lat, lon = request.lon }));
    }
}
