using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Extensions;
using MediatR;
using Services.DataProcessService.Models;
using Services.DataProcessService.Aggregate.ValueObjects;

namespace Services.DataProcessService.Features.Queries.Current.GetAirPollutionInMemory
{
    public class GetCurrentWeatherInMemoryQueryHandler : IRequestHandler<GetCurrentWeatherInMemoryQueryRequest, GetCurrentWeatherInMemoryQueryResponse>
    {
        private readonly IRedisService<Aggregate.CurrentWeather, CurrentWeatherId> _redisService;

        public GetCurrentWeatherInMemoryQueryHandler(IRedisService<Aggregate.CurrentWeather, CurrentWeatherId> redisService)
        {
            _redisService = redisService;
        }

        public async Task<GetCurrentWeatherInMemoryQueryResponse> Handle(GetCurrentWeatherInMemoryQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                string key = KeyFormatterExtension.Format(nameof(CurrentWeatherModel), request.coord.lat, request.coord.lon);
                CurrentWeatherModel? currentWeatherModel = _redisService.Get<CurrentWeatherModel>(key);
                return new(currentWeatherModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
