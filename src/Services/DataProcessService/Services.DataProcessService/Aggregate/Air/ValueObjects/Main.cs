using BuildingBlock.Base.Models.Base;

namespace Services.DataProcessService.Aggregate.Air.ValueObjects
{
    public class Main : ValueObject
    {
        public int Aqi { get; set; }

        public Main(int aqi)
        {
            Aqi = aqi;
        }

        public static Main Create(int aqi)
            => new(aqi);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Aqi;
        }
    }
}
