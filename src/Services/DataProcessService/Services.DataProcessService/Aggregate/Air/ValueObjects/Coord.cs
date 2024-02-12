using BuildingBlock.Base.Models.Base;

namespace Services.DataProcessService.Aggregate.Air.ValueObjects
{
    public class Coord : ValueObject
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Coord(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public static Coord Create(double latitude, double longitude)
            => new(latitude, longitude);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}
