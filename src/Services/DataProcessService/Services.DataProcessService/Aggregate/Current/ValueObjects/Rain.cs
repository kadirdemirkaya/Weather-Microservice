using BuildingBlock.Base.Models.Base;

namespace Services.DataProcessService.Aggregate.Current.ValueObjects
{
    public class Rain : ValueObject
    {
        public double _1h { get; set; }

        public Rain(double _1h)
        {
            this._1h = _1h;
        }

        public static Rain Create(double _1h)
            => new(_1h);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return _1h;
        }
    }
}
