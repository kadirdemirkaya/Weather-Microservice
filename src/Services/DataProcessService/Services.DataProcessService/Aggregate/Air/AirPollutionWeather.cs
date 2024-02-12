using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Models.Base;
using Services.DataProcessService.Aggregate.Air.ValueObjects;

namespace Services.DataProcessService.Aggregate.Air
{
    public class AirPollutionWeather : AggregateRoot<AirPollutionWeatherId>
    {
        public Coord Coord { get; set; }

        private readonly List<Aggregate.Air.Entities.AList> Alists = new();
        public IReadOnlyCollection<Aggregate.Air.Entities.AList> ALists => Alists.AsReadOnly();

        public AirPollutionWeather()
        {

        }

        public AirPollutionWeather(Coord coord)
        {
            Id = AirPollutionWeatherId.CreateUnique();
            Coord = coord;
        }

        public AirPollutionWeather(AirPollutionWeatherId airPollutionWeatherId, Coord coord) : base(airPollutionWeatherId)
        {
            Id = airPollutionWeatherId;
            Coord = coord;
        }

        public static AirPollutionWeather Create(Coord coord)
            => new(coord);

        public static AirPollutionWeather Create(Guid Id, Coord coord)
            => new(AirPollutionWeatherId.Create(Id), coord);

        public static AirPollutionWeather Create(AirPollutionWeatherId airPollutionWeatherId, Coord coord)
            => new(airPollutionWeatherId, coord);

        public void AddList(AListId listId, int dt, Main main, Component components, AirPollutionWeatherId airPollutionWeatherId)
        {
            Alists.Add(Aggregate.Air.Entities.AList.Create(listId, dt, main, components, airPollutionWeatherId));
        }
        public void AddList(List<Aggregate.Air.Entities.AList> lists)
        {
            lists.AddRange(lists);
        }

        public void AddUserDomainEvent(IDomainEvent @event)
        {
            AddDomainEvent(@event);
        }

        public void CountDomainEvent() => DomainEvents.Count();
    }
}
