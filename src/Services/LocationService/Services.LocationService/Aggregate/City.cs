using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Models.Base;
using Services.LocationService.Aggregate.ValueObjects;

namespace Services.LocationService.Aggregate
{
    public class City : AggregateRoot<CityId>
    {
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        public City()
        {

        }

        public City(string cityName, string countryName, double latitude, double longitude)
        {
            Id = CityId.CreateUnique();
            CountryName = countryName;
            CityName = cityName;
            Latitude = latitude;
            Longitude = longitude;
        }

        public City(CityId cityId, string cityName, string countryName, double latitude, double longitude) : base(cityId)
        {
            Id = cityId;
            CountryName = countryName;
            CityName = cityName;
            Latitude = latitude;
            Longitude = longitude;
        }

        public static City Create(string cityName, string countryName, double latitude, double longitude)
            => new(cityName, countryName, latitude, longitude);

        public static City Create(CityId cityId, string cityName, string countryName, double latitude, double longitude)
            => new(cityId, cityName, countryName, latitude, longitude);

        public void AddUserDomainEvent(IDomainEvent @event)
        {
            AddDomainEvent(@event);
        }

        public void CountDomainEvent() => DomainEvents.Count();
    }
}
