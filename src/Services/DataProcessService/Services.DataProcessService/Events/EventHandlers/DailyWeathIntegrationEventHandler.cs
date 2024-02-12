using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Exceptions;
using Serilog;
using Services.DataProcessService.Aggregate.Air.Events;
using Services.DataProcessService.Aggregate.Daily;
using Services.DataProcessService.Aggregate.Daily.Entities;
using Services.DataProcessService.Aggregate.Daily.Events;
using Services.DataProcessService.Aggregate.Daily.ValueObjects;

namespace Services.DataProcessService.Events.EventHandlers
{
    public class DailyWeathIntegrationEventHandler : IIntegrationEventHandler<DailyWeathIntegrationEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private bool res = false;
        public DailyWeathIntegrationEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DailyWeathIntegrationEvent @event)
        {
            DailyWeather dailyWeather = DailyWeather.Create(DailyWeatherId.CreateUnique(), @event.WeatherData.cod, @event.WeatherData.message, @event.WeatherData.cnt, City.Create(@event.WeatherData.city.id, @event.WeatherData.city.name, @event.WeatherData.city.coord.lon, @event.WeatherData.city.coord.lat, @event.WeatherData.city.country, @event.WeatherData.city.population, @event.WeatherData.city.timezone, @event.WeatherData.city.sunrise, @event.WeatherData.city.sunset));

            foreach (var lst in @event.WeatherData.list)
            {
                var main = Main.Create(lst.main.temp, lst.main.feels_like, lst.main.temp_min, lst.main.temp_max, lst.main.pressure, lst.main.humidity, lst.main.sea_level, lst.main.grnd_level, lst.main.temp_kf);
                var cloud = Cloud.Create(lst.clouds.all);
                var wind = Wind.Create(lst.wind.speed, lst.wind.deg, lst.wind.gust);
                var rain = Rain.Create(lst.rain._3h);
                var sys = Sys.Create(lst.sys.pod);
                var listId = DListId.CreateUnique();
                List<DWeather> dweathers = new();

                foreach (var weat in lst.weather)
                {
                    dweathers.Add(DWeather.Create(weat.id, weat.main, weat.description, weat.icon, listId));
                }

                dailyWeather.AddListWithDWeather(listId, lst.dt, main, cloud, wind, lst.visibility, lst.pop, rain, sys, lst.dt_txt, dailyWeather.Id, dweathers);
            }

            var anyWeather = await _unitOfWork.GetReadRepository<DailyWeather, DailyWeatherId>().GetAsync(d => d.City.Lat == dailyWeather.City.Lat && d.City.Lon == dailyWeather.City.Lon);

            try
            {
                if (anyWeather is not null)
                    if (_unitOfWork.GetWriteRepository<DailyWeather, DailyWeatherId>().Delete(anyWeather))
                        if (await _unitOfWork.GetWriteRepository<DailyWeather, DailyWeatherId>().CreateAsync(dailyWeather))
                            res = await _unitOfWork.SaveChangesAsync() > 0;

                if (await _unitOfWork.GetWriteRepository<DailyWeather, DailyWeatherId>().CreateAsync(dailyWeather))
                    res = await _unitOfWork.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Log.Error("Event Error : " + ex.Message);
                throw new EventErrorException(ex.Message, nameof(AirWeathIntegrationEvent));
            }
        }
    }
}
