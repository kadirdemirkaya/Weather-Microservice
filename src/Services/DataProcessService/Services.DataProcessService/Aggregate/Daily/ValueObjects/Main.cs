using BuildingBlock.Base.Models.Base;

namespace Services.DataProcessService.Aggregate.Daily.ValueObjects
{
    public class Main : ValueObject
    {
        public double Temp { get; set; }
        public double Feels_like { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public double Sea_level { get; set; }
        public double Grnd_level { get; set; }
        public double Temp_kf { get; set; }

        public Main(double temp, double feels_like, double temp_min, double temp_max, int pressure, int humidity, double sea_level, double grnd_level, double temp_kf)
        {
            Temp = temp;
            Feels_like = feels_like;
            Temp_min = temp_min;
            Temp_max = temp_max;
            Pressure = pressure;
            Humidity = humidity;
            Sea_level = sea_level;
            Grnd_level = grnd_level;
            Temp_kf = temp_kf;
        }

        public static Main Create(double temp, double feels_like, double temp_min, double temp_max, int pressure, int humidity, double sea_level, double grnd_level, double temp_kf)
            => new(temp, feels_like, temp_min, temp_max, pressure, humidity, sea_level, grnd_level, temp_kf);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Temp;
            yield return Feels_like;
            yield return Temp_min;
            yield return Temp_max;
            yield return Pressure;
            yield return Humidity;
            yield return Sea_level;
            yield return Grnd_level;
            yield return Temp_kf;
        }
    }
}
