using BuildingBlock.Base.Models.Base;

namespace Services.DataProcessService.Aggregate.Current.ValueObjects
{
    public class Coord : ValueObject
    {
        public double Lon { get; set; }
        public double Lat { get; set; }

        public Coord(double lon, double lat)
        {
            Lon = lon;
            Lat = lat;
        }

        public static Coord Create(double lon, double lat)
            => new(lon, lat);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Lon;
            yield return Lat;
        }
    }
}
