using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Models.Base;
using Services.DataProcessService.Aggregate.Daily.Entities;
using Services.DataProcessService.Aggregate.Daily.ValueObjects;

namespace Services.DataProcessService.Aggregate.Daily
{
    public class DailyWeather : AggregateRoot<DailyWeatherId>
    {
        public string Cod { get; set; }
        public int Message { get; set; }
        public int Cnt { get; set; }
        public City City { get; set; }


        private readonly List<Daily.Entities.DList> Dlists = new();
        public IReadOnlyCollection<Daily.Entities.DList> DLists => Dlists.AsReadOnly();


        public DailyWeather()
        {

        }
        public DailyWeather(string cod, int message, int cnt, City city)
        {
            Id = DailyWeatherId.CreateUnique();
            Cod = cod;
            Message = message;
            Cnt = cnt;
            City = city;
        }
        public DailyWeather(DailyWeatherId dailyWeatherId, string cod, int message, int cnt, City city) : base(dailyWeatherId)
        {
            Id = dailyWeatherId;
            Cod = cod;
            Message = message;
            Cnt = cnt;
            City = city;
        }

        public static DailyWeather Create(string cod, int message, int cnt, City city)
            => new(DailyWeatherId.CreateUnique(), cod, message, cnt, city);

        public static DailyWeather Create(Guid Id, string cod, int message, int cnt, City city)
            => new(DailyWeatherId.Create(Id), cod, message, cnt, city);

        public static DailyWeather Create(DailyWeatherId dailyWeatherId, string cod, int message, int cnt, City city)
            => new(dailyWeatherId, cod, message, cnt, city);

        public void AddList(DListId listId, int dt, Main main, Cloud clouds, Wind wind, int visibility, double pop, Rain rain, Sys sys, string dt_txt, DailyWeatherId dailyWeatherId)
        {
            Dlists.Add(Daily.Entities.DList.Create(listId, dt, main, clouds, wind, visibility, pop, rain, sys, dt_txt, dailyWeatherId));
        }

        public void AddListWithDWeather(DListId listId, int dt, Main main, Cloud clouds, Wind wind, int visibility, double pop, Rain rain, Sys sys, string dt_txt, DailyWeatherId dailyWeatherId, DWeather dWeather)
        {
            var _list = Daily.Entities.DList.Create(listId, dt, main, clouds, wind, visibility, pop, rain, sys, dt_txt, dailyWeatherId);
            _list.AddWeatherWithDWeather(dWeather);
            Dlists.Add(_list);
        }

        public void AddListWithDWeather(DListId listId, int dt, Main main, Cloud clouds, Wind wind, int visibility, double pop, Rain rain, Sys sys, string dt_txt, DailyWeatherId dailyWeatherId, List<DWeather> dWeathers)
        {
            var _list = Daily.Entities.DList.Create(listId, dt, main, clouds, wind, visibility, pop, rain, sys, dt_txt, dailyWeatherId);
            _list.AddWeatherWithDWeather(dWeathers);
            Dlists.Add(_list);
        }


        public void AddUserDomainEvent(IDomainEvent @event)
        {
            AddDomainEvent(@event);
        }

        public void CountDomainEvent() => DomainEvents.Count();
    }
}
