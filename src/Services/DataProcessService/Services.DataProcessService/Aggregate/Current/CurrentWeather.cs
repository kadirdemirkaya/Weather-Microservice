using BuildingBlock.Base.Models.Base;
using Services.DataProcessService.Aggregate.Current.Entities;
using Services.DataProcessService.Aggregate.Current.ValueObjects;
using Services.DataProcessService.Aggregate.ValueObjects;

namespace Services.DataProcessService.Aggregate
{
    public class CurrentWeather : AggregateRoot<CurrentWeatherId>
    {
        public Coord Coord { get; set; }
        public string @Base { get; set; }
        public Main Main { get; set; }
        public int Visibility { get; set; }
        public Wind Wind { get; set; }
        public Rain? Rain { get; set; }
        public Cloud Cloud { get; set; }
        public int Dt { get; set; }
        public Sys Sys { get; set; }
        public int Timezone { get; set; }
        public int id { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }


        public readonly List<CWeather> _cWeathers = new();
        public IReadOnlyCollection<CWeather> CWeathers => _cWeathers.AsReadOnly();

        public CurrentWeather()
        {

        }
        public CurrentWeather(Coord coord, string @base, Main main, int visibility, Wind wind, Rain rain, Cloud cloud, int dt, Sys sys, int timezone, int id, string name, int cod)
        {
            Id = CurrentWeatherId.CreateUnique();
            Coord = coord;
            Base = @base;
            Main = main;
            Visibility = visibility;
            Wind = wind;
            Rain = rain;
            Cloud = cloud;
            Dt = dt;
            Sys = sys;
            Timezone = timezone;
            this.id = id;
            Name = name;
            Cod = cod;
        }

        public CurrentWeather(CurrentWeatherId currentWeatherId, Coord coord, string @base, Main main, int visibility, Wind wind, Rain rain, Cloud cloud, int dt, Sys sys, int timezone, int id, string name, int cod) : base(currentWeatherId)
        {
            Id = currentWeatherId;
            Coord = coord;
            Base = @base;
            Main = main;
            Visibility = visibility;
            Wind = wind;
            Rain = rain;
            Cloud = cloud;
            Dt = dt;
            Sys = sys;
            Timezone = timezone;
            this.id = id;
            Name = name;
            Cod = cod;
        }

        public static CurrentWeather CreateCurrentWeather(CurrentWeatherId currentWeatherId, Coord coord, string @base, Main main, int visibility, Wind wind, Rain rain, Cloud cloud, int dt, Sys sys, int timezone, int id, string name, int cod)
            => new(currentWeatherId, coord, @base, main, visibility, wind, rain, cloud, dt, sys, timezone, id, name, cod);

        public static CurrentWeather CreateCurrentWeather(Guid currentWeatherId, Coord coord, string @base, Main main, int visibility, Wind wind, Rain rain, Cloud cloud, int dt, Sys sys, int timezone, int id, string name, int cod)
            => new(CurrentWeatherId.Create(currentWeatherId), coord, @base, main, visibility, wind, rain, cloud, dt, sys, timezone, id, name, cod);

        public static CurrentWeather CreateCurrentWeather(Coord coord, string @base, Main main, int visibility, Wind wind, Rain rain, Cloud cloud, int dt, Sys sys, int timezone, int id, string name, int cod)
             => new(coord, @base, main, visibility, wind, rain, cloud, dt, sys, timezone, id, name, cod);

        public void AddWeather(WeatherId weatherId, int id, string main, string description, string icon, CurrentWeatherId currentWeatherId)
        {
            _cWeathers.Add(CWeather.Create(weatherId, id, main, description, icon, currentWeatherId));
        }

        public void DeleteWeather()
        {
            _cWeathers.RemoveRange(0, _cWeathers.Count());
        }

        public void CountDomainEvent() => DomainEvents.Count();
    }
}
