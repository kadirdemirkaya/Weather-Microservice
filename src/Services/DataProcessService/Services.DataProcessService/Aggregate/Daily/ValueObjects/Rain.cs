using BuildingBlock.Base.Models.Base;

namespace Services.DataProcessService.Aggregate.Daily.ValueObjects
{
    public class Rain : ValueObject
    {
        public double? _3h { get; set; } = 0;
        public Rain(double? _3h)
        {
            this._3h = _3h;
        }

        public static Rain Create(double _3h)
            => new(_3h);

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return _3h;
        }
    }
}
