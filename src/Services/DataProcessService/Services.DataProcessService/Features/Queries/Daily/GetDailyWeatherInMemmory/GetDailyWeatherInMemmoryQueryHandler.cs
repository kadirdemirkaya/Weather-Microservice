using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Extensions;
using MediatR;
using Services.DataProcessService.Aggregate.Daily.ValueObjects;
using Services.DataProcessService.Models.Daily;

namespace Services.DataProcessService.Features.Queries.Daily.GetDailyWeatherInMemmory
{
    public class GetDailyWeatherInMemmoryQueryHandler : IRequestHandler<GetDailyWeatherInMemmoryQueryRequest, GetDailyWeatherInMemmoryQueryResponse>
    {
        private readonly IRedisService<Aggregate.Daily.DailyWeather, DailyWeatherId> _redisService;

        public GetDailyWeatherInMemmoryQueryHandler(IRedisService<Aggregate.Daily.DailyWeather, DailyWeatherId> redisService)
        {
            _redisService = redisService;
        }

        public async Task<GetDailyWeatherInMemmoryQueryResponse> Handle(GetDailyWeatherInMemmoryQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                string key = KeyFormatterExtension.Format(nameof(DailyWeatherModel), request.Coord.lat,request.Coord.lon);
                DailyWeatherModel? dailyWeatherModel = _redisService.Get<DailyWeatherModel>(key);
                return new(dailyWeatherModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
