using MediatR;
using Services.DataProcessService.Abstractions;

namespace Services.DataProcessService.Features.Queries.Daily.DailyWeather
{
    public class DailyWeatherQueryHandler : IRequestHandler<DailyWeatherQueryRequest, DailyWeatherQueryResponse>
    {
        private readonly IDailyWeatherService _dailyWeatherService;

        public DailyWeatherQueryHandler(IDailyWeatherService dailyWeatherService)
        {
            _dailyWeatherService = dailyWeatherService;
        }

        public async Task<DailyWeatherQueryResponse> Handle(DailyWeatherQueryRequest request, CancellationToken cancellationToken)
            => new(await _dailyWeatherService.GetDWeatherModelAsync(new Models.Coord() { lat = request.lat, lon = request.lon }));
    }
}
