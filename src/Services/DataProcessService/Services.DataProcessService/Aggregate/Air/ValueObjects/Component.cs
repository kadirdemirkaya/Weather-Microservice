using BuildingBlock.Base.Models.Base;

namespace Services.DataProcessService.Aggregate.Air.ValueObjects
{
    public class Component : ValueObject
    {
        public double Co { get; set; }
        public double No { get; set; }
        public double No2 { get; set; }
        public double O3 { get; set; }
        public double So2 { get; set; }
        public double Pm2 { get; set; }
        public double Pm10 { get; set; }
        public double Nh3 { get; set; }

        public Component(double co, double no, double no2, double o3, double so2, double pm2, double pm10, double nh3)
        {
            Co = co;
            No = no;
            No2 = no2;
            O3 = o3;
            So2 = so2;
            Pm2 = pm2;
            Pm10 = pm10;
            Nh3 = nh3;
        }

        public static Component Create(double co, double no, double no2, double o3, double so2, double pm2, double pm10, double nh3)
            => new(co, no, no2, o3, so2, pm2, pm10, nh3);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Co;
            yield return No;
            yield return No2;
            yield return O3;
            yield return So2;
            yield return Pm2;
            yield return Pm10;
            yield return Nh3;
        }
    }
}
