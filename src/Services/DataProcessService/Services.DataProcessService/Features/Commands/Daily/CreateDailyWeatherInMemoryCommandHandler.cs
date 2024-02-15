using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Extensions;
using MediatR;
using Services.DataProcessService.Aggregate.Daily;
using Services.DataProcessService.Aggregate.Daily.ValueObjects;
using Services.DataProcessService.Models.Daily;

namespace Services.DataProcessService.Features.Commands.Daily
{
    public class CreateDailyWeatherInMemoryCommandHandler : IRequestHandler<CreateDailyWeatherInMemoryCommandRequest, CreateDailyWeatherInMemoryCommandResponse>
    {
        private readonly IRedisService<DailyWeather, DailyWeatherId> _redisService;

        public CreateDailyWeatherInMemoryCommandHandler(IRedisService<DailyWeather, DailyWeatherId> redisService)
        {
            _redisService = redisService;
        }

        public async Task<CreateDailyWeatherInMemoryCommandResponse> Handle(CreateDailyWeatherInMemoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                int count = _redisService.GetStringKeyCount();
                if (count == 10)
                    return new(true);
                string key = KeyFormatterExtension.Format(nameof(DailyWeatherModel), request.coord.lat, request.coord.lon);
                return new(_redisService.Add(key, request.DailyWeatherModel, TimeSpanExtension.AddMinute(200)));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
