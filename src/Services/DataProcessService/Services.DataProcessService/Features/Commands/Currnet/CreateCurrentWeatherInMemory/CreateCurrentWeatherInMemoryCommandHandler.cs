using Services.DataProcessService.Models;
using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Extensions;
using MediatR;
using Services.DataProcessService.Aggregate;
using Services.DataProcessService.Aggregate.ValueObjects;

namespace Services.DataProcessService.Features.Commands.Currnet.CreateCurrentWeatherInMemory
{
    public class CreateCurrentWeatherInMemoryCommandHandler : IRequestHandler<CreateCurrentWeatherInMemoryCommandRequest, CreateCurrentWeatherInMemoryCommandResponse>
    {
        private readonly IRedisService<CurrentWeather, CurrentWeatherId> _redisService;

        public CreateCurrentWeatherInMemoryCommandHandler(IRedisService<CurrentWeather, CurrentWeatherId> redisService)
        {
            _redisService = redisService;
        }

        public async Task<CreateCurrentWeatherInMemoryCommandResponse> Handle(CreateCurrentWeatherInMemoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                int count = _redisService.GetStringKeyCount();
                if (count == 10)
                    return new(true);
                string key = KeyFormatterExtension.Format(nameof(CurrentWeatherModel), request.coord.lat, request.coord.lon);
                return new(_redisService.Add(key, request.CurrentWeatherModel, TimeSpanExtension.AddMinute(200)));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
