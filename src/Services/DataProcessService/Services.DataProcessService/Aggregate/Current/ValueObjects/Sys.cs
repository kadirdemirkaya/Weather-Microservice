using BuildingBlock.Base.Models.Base;

namespace Services.DataProcessService.Aggregate.Current.ValueObjects
{
    public class Sys : ValueObject
    {
        public int Type { get; set; }
        public int id { get; set; }
        public string Country { get; set; }
        public int Sunrise { get; set; }

        public Sys(int type, int id, string country, int sunrise)
        {
            Type = type;
            this.id = id;
            Country = country;
            Sunrise = sunrise;
        }

        public static Sys Create(int type, int id, string country, int sunrise)
            => new(type, id, country, sunrise);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return id;
            yield return Country;
            yield return Sunrise;
        }
    }
}
