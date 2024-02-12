using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Models.Base;
using Services.DataProcessService.Aggregate.Air.ValueObjects;

namespace Services.DataProcessService.Aggregate.Air.Entities
{
    public class AList : Entity<AListId>
    {
        public int Dt { get; set; }
        public Main Main { get; set; }
        public Component Components { get; set; }


        public AirPollutionWeatherId AirPollutionWeatherId { get; set; }
        public AirPollutionWeather AirPollutionWeather { get; set; }

        public AList()
        {

        }
        public AList(int dt, Main main, Component components, AirPollutionWeatherId airPollutionWeatherId)
        {
            Id = AListId.CreateUnique();
            Dt = dt;
            Main = main;
            Components = components;
            AirPollutionWeatherId = airPollutionWeatherId;
        }
        public AList(AListId listId, int dt, Main main, Component components, AirPollutionWeatherId airPollutionWeatherId) : base(listId)
        {
            Id = AListId.CreateUnique();
            Dt = dt;
            Main = main;
            Components = components;
            AirPollutionWeatherId = airPollutionWeatherId;
        }

        public static AList Create(int dt, Main main, Component components, AirPollutionWeatherId airPollutionWeatherId)
            => new(dt, main, components, airPollutionWeatherId);
        public static AList Create(Guid listId, int dt, Main main, Component components, AirPollutionWeatherId airPollutionWeatherId)
            => new(AListId.Create(listId), dt, main, components, airPollutionWeatherId);
        public static AList Create(AListId listId, int dt, Main main, Component components, AirPollutionWeatherId airPollutionWeatherId)
            => new(listId, dt, main, components, airPollutionWeatherId);

        public void AddUserDomainEvent(IDomainEvent @event)
        {
            AddDomainEvent(@event);
        }

        public void CountDomainEvent() => DomainEvents.Count();
    }
}
