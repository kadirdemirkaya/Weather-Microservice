using BuildingBlock.Base.Models.Base;

namespace Services.DataProcessService.Aggregate.Current.ValueObjects
{
    public class Wind : ValueObject
    {
        public double Speed { get; set; }
        public int Deg { get; set; }
        public double Gust { get; set; }

        public Wind(double speed, int deg, double gust)
        {
            Speed = speed;
            Deg = deg;
            Gust = gust;
        }

        public static Wind Create(double speed, int deg, double gust)
            => new(speed, deg, gust);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Speed;
            yield return Deg;
            yield return Gust;
        }
    }
}
