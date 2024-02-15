using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Exceptions;
using BuildingBlock.Redis;
using Serilog;
using Services.DataProcessService.Aggregate.Air;
using Services.DataProcessService.Aggregate.Air.Events;
using Services.DataProcessService.Aggregate.Air.ValueObjects;
using Services.DataProcessService.Constants;

namespace Services.DataProcessService.Events.EventHandlers
{
    public class AirWeathIntegrationEventHandler : IIntegrationEventHandler<AirWeathIntegrationEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private bool res = false;
        private readonly IRedisService<AirPollutionWeather, AirPollutionWeatherId> _redisService;
        private string key;
        public AirWeathIntegrationEventHandler(IUnitOfWork unitOfWork, IRedisService<AirPollutionWeather, AirPollutionWeatherId> redisService)
        {
            _unitOfWork = unitOfWork;
            _redisService = redisService;
            key = Constant.Keys.AirPollutionModel;
        }

        public async Task Handle(AirWeathIntegrationEvent @event)
        {
            _redisService.DeleteKeys(key);

            AirPollutionWeather airPollutionWeather = AirPollutionWeather.Create(AirPollutionWeatherId.CreateUnique(), Coord.Create(@event.WeatherData.coord.lat, @event.WeatherData.coord.lon));

            foreach (var lst in @event.WeatherData.list)
            {
                airPollutionWeather.AddList(AListId.CreateUnique(), lst.dt, Main.Create(lst.main.aqi), Component.Create(lst.components.co, lst.components.no, lst.components.no2, lst.components.o3, lst.components.so2, lst.components.pm2, lst.components.pm10, lst.components.nh3), airPollutionWeather.Id);
            }

            var anyData = await _unitOfWork.GetReadRepository<AirPollutionWeather, AirPollutionWeatherId>().GetAsync(a => a.Coord.Latitude == airPollutionWeather.Coord.Latitude && a.Coord.Longitude == airPollutionWeather.Coord.Longitude);

            try
            {
                if (anyData is not null)
                {
                    airPollutionWeather.Id = anyData.Id;
                    if (_unitOfWork.GetWriteRepository<AirPollutionWeather, AirPollutionWeatherId>().Delete(anyData))
                        if (await _unitOfWork.GetWriteRepository<AirPollutionWeather, AirPollutionWeatherId>().CreateAsync(airPollutionWeather))
                            res = await _unitOfWork.SaveChangesAsync() > 0;
                }
                else
                {
                    if (await _unitOfWork.GetWriteRepository<AirPollutionWeather, AirPollutionWeatherId>().CreateAsync(airPollutionWeather))
                        res = await _unitOfWork.SaveChangesAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Event Error : " + ex.Message);
                throw new EventErrorException(ex.Message, nameof(AirWeathIntegrationEvent));
            }

        }
    }
}
