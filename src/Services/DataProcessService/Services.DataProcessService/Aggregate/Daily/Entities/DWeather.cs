using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Models.Base;
using Services.DataProcessService.Aggregate.Daily.ValueObjects;

namespace Services.DataProcessService.Aggregate.Daily.Entities
{
    public class DWeather : Entity<WeatherId>
    {
        public int id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public DListId DListId { get; set; }
        public DList DList { get; set; }

        public DWeather()
        {

        }

        public DWeather(int id, string main, string description, string icon, DListId listId)
        {
            Id = WeatherId.CreateUnique();
            this.id = id;
            Main = main;
            Description = description;
            Icon = icon;
            DListId = listId;
        }

        public DWeather(WeatherId weatherId, int id, string main, string description, string icon, DListId listId) : base(weatherId)
        {
            Id = weatherId;
            this.id = id;
            Main = main;
            Description = description;
            Icon = icon;
            DListId = listId;
        }

        public static DWeather Create(int id, string main, string description, string icon, DListId listId)
            => new(WeatherId.CreateUnique(), id, main, description, icon, listId);

        public static DWeather Create(Guid Id, int id, string main, string description, string icon, DListId listId)
            => new(WeatherId.Create(Id), id, main, description, icon, listId);

        public static DWeather Create(WeatherId weatherId, int id, string main, string description, string icon, DListId listId)
            => new(weatherId, id, main, description, icon, listId);

        public void AddUserDomainEvent(IDomainEvent @event)
        {
            AddDomainEvent(@event);
        }

        public void CountDomainEvent() => DomainEvents.Count();
    }
}
