using BuildingBlock.Base.Models.Base;

namespace Services.DataProcessService.Aggregate.Daily.ValueObjects
{
    public class City : ValueObject
    {
        public int id { get; set; }
        public string Name { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
        public int Timezone { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }

        public City(int id, string name, double lon, double lat, string country, int population, int timezone, int sunrise, int sunset)
        {
            this.id = id;
            Name = name;
            Lon = lon;
            Lat = lat;
            Country = country;
            Population = population;
            Timezone = timezone;
            Sunrise = sunrise;
            Sunset = sunset;
        }

        public static City Create(int id, string name, double lon, double lat, string country, int population, int timezone, int sunrise, int sunset)
            => new(id, name, lon, lat, country, population, timezone, sunrise, sunset);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return id;
            yield return Name;
            yield return Lon;
            yield return Lat;
            yield return Country;
            yield return Population;
            yield return Timezone;
            yield return Sunrise;
            yield return Sunset;
        }
    }
}
