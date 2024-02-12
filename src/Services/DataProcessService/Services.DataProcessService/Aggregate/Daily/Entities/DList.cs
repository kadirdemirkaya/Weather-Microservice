using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Models.Base;
using Services.DataProcessService.Aggregate.Daily.ValueObjects;

namespace Services.DataProcessService.Aggregate.Daily.Entities
{
    public class DList : Entity<DListId>
    {
        public int Dt { get; set; }
        public Main Main { get; set; }
        public Cloud Clouds { get; set; }
        public Wind Wind { get; set; }
        public int Visibility { get; set; }
        public double Pop { get; set; }
        public Rain Rain { get; set; }
        public Sys Sys { get; set; }
        public string Dt_txt { get; set; }


        public DailyWeatherId DailyWeatherId { get; set; }
        public DailyWeather DailyWeather { get; set; }


        private readonly List<DWeather> _dWeather = new();
        public IReadOnlyCollection<DWeather> Dweather => _dWeather.AsReadOnly();


        public DList()
        {

        }
        public DList(int dt, Main main, Cloud clouds, Wind wind, int visibility, double pop, Rain rain, Sys sys, string dt_txt, DailyWeatherId dailyWeatherId)
        {
            Id = DListId.CreateUnique();
            Dt = dt;
            Main = main;
            Clouds = clouds;
            Wind = wind;
            Visibility = visibility;
            Pop = pop;
            Rain = rain;
            Sys = sys;
            Dt_txt = dt_txt;
            DailyWeatherId = dailyWeatherId;
        }

        public DList(DListId listId, int dt, Main main, Cloud clouds, Wind wind, int visibility, double pop, Rain rain, Sys sys, string dt_txt, DailyWeatherId dailyWeatherId) : base(listId)
        {
            Id = listId;
            Dt = dt;
            Main = main;
            Clouds = clouds;
            Wind = wind;
            Visibility = visibility;
            Pop = pop;
            Rain = rain;
            Sys = sys;
            Dt_txt = dt_txt;
            DailyWeatherId = dailyWeatherId;
        }

        public static DList Create(int dt, Main main, Cloud clouds, Wind wind, int visibility, double pop, Rain rain, Sys sys, string dt_txt, DailyWeatherId dailyWeatherId)
            => new(dt, main, clouds, wind, visibility, pop, rain, sys, dt_txt, dailyWeatherId);

        public static DList Create(Guid Id, int dt, Main main, Cloud clouds, Wind wind, int visibility, double pop, Rain rain, Sys sys, string dt_txt, DailyWeatherId dailyWeatherId)
            => new(DListId.Create(Id), dt, main, clouds, wind, visibility, pop, rain, sys, dt_txt, dailyWeatherId);

        public static DList Create(DListId listId, int dt, Main main, Cloud clouds, Wind wind, int visibility, double pop, Rain rain, Sys sys, string dt_txt, DailyWeatherId dailyWeatherId)
            => new(listId, dt, main, clouds, wind, visibility, pop, rain, sys, dt_txt, dailyWeatherId);

        public void AddWeather(WeatherId weatherId, int id, string main, string description, string icon, DListId listId)
        {
            _dWeather.Add(DWeather.Create(weatherId, id, main, description, icon, listId));
        }
        public void AddWeatherWithDWeather(DWeather dWeathers)
        {
            _dWeather.Add(dWeathers);
        }
        public void AddWeatherWithDWeather(List<DWeather> dWeathers)
        {
            _dWeather.AddRange(dWeathers);
        }

        public void AddUserDomainEvent(IDomainEvent @event)
        {
            AddDomainEvent(@event);
        }

        public void CountDomainEvent() => DomainEvents.Count();
    }
}
