using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Exceptions;
using Serilog;
using Services.DataProcessService.Aggregate;
using Services.DataProcessService.Aggregate.Air.Events;
using Services.DataProcessService.Aggregate.Current.Events;
using Services.DataProcessService.Aggregate.Current.ValueObjects;
using Services.DataProcessService.Aggregate.ValueObjects;

namespace Services.DataProcessService.Events.EventHandlers
{
    public class CurrentWeathIntegrationEventHandler : IIntegrationEventHandler<CurrentWeathIntegrationEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private bool res = false;

        public CurrentWeathIntegrationEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CurrentWeathIntegrationEvent @event)
        {
            //var currentWeather = _mapper.Map<Services.DataProcessService.Aggregate.CurrentWeather>(@event.WeatherData);

            CurrentWeather currentWeatherEnt = CurrentWeather.CreateCurrentWeather(Coord.Create
                (@event.WeatherData.coord.lon, @event.WeatherData.coord.lat), @event.WeatherData.@base, Main.Create(@event.WeatherData.main.temp, @event.WeatherData.main.feels_like, @event.WeatherData.main.temp_min, @event.WeatherData.main.temp_max, @event.WeatherData.main.pressure, @event.WeatherData.main.humidity, @event.WeatherData.main.sea_level, @event.WeatherData.main.grnd_level), @event.WeatherData.visibility, Wind.Create(@event.WeatherData.wind.speed, @event.WeatherData.wind.deg, @event.WeatherData.wind.gust), Rain.Create(@event.WeatherData.rain._1h), Cloud.Create(@event.WeatherData.clouds.all), @event.WeatherData.dt, Sys.Create(@event.WeatherData.sys.type, @event.WeatherData.sys.id, @event.WeatherData.sys.country, @event.WeatherData.sys.sunrise), @event.WeatherData.timezone, @event.WeatherData.id, @event.WeatherData.name, @event.WeatherData.cod);

            foreach (var weather in @event.WeatherData.weather)
            {
                currentWeatherEnt.AddWeather(WeatherId.CreateUnique(), weather.id, weather.main, weather.description, weather.icon, currentWeatherEnt.Id);
            }

            var anyWeather = await _unitOfWork.GetReadRepository<CurrentWeather, CurrentWeatherId>().GetAsync(c => c.Coord.Lat == @event.WeatherData.coord.lat && c.Coord.Lon == @event.WeatherData.coord.lon);

            try
            {
                if (anyWeather is not null)
                {
                    if (_unitOfWork.GetWriteRepository<CurrentWeather, CurrentWeatherId>().Delete(anyWeather))
                        if (await _unitOfWork.GetWriteRepository<CurrentWeather, CurrentWeatherId>().CreateAsync(currentWeatherEnt))
                            res = await _unitOfWork.SaveChangesAsync() > 0;
                }
                else
                {
                    if (await _unitOfWork.GetWriteRepository<CurrentWeather, CurrentWeatherId>().CreateAsync(currentWeatherEnt))
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
