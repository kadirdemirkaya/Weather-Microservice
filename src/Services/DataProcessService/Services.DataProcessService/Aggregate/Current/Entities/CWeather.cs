using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Models.Base;
using Services.DataProcessService.Aggregate.Current.ValueObjects;
using Services.DataProcessService.Aggregate.ValueObjects;

namespace Services.DataProcessService.Aggregate.Current.Entities
{
    public class CWeather : Entity<WeatherId>
    {
        public int id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }


        public CurrentWeatherId CurrentWeatherId { get; set; }
        public CurrentWeather CurrentWeather { get; set; }

        public CWeather()
        {

        }

        public CWeather(int id, string main, string description, string icon, CurrentWeatherId currentWeatherId)
        {
            Id = WeatherId.CreateUnique();
            this.id = id;
            Main = main;
            Description = description;
            Icon = icon;
        }
        public CWeather(WeatherId weatherId, int id, string main, string description, string icon, CurrentWeatherId currentWeatherId) : base(weatherId)
        {
            Id = weatherId;
            this.id = id;
            Main = main;
            Description = description;
            Icon = icon;
        }

        public static CWeather Create(int id, string main, string description, string icon, CurrentWeatherId currentWeatherId)
            => new(id, main, description, icon,currentWeatherId);

        public static CWeather Create(Guid _id, int id, string main, string description, string icon, CurrentWeatherId currentWeatherId)
            => new(WeatherId.Create(_id), id, main, description, icon,currentWeatherId);

        public static CWeather Create(WeatherId weatherId, int id, string main, string description, string icon, CurrentWeatherId currentWeatherId)
            => new(weatherId, id, main, description, icon,currentWeatherId);

        public void AddUserDomainEvent(IDomainEvent @event)
        {
            AddDomainEvent(@event);
        }

        public void CountDomainEvent() => DomainEvents.Count();
    }
}
