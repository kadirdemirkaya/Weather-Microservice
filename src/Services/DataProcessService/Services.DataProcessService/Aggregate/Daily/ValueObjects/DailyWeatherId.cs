using BuildingBlock.Base.Models.Base;

namespace Services.DataProcessService.Aggregate.Daily.ValueObjects
{
    public sealed class DailyWeatherId : ValueObject
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public DailyWeatherId(Guid id)
        {
            Id = id;
        }

        public static DailyWeatherId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static DailyWeatherId Create(Guid Id)
        {
            return new DailyWeatherId(Id);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
