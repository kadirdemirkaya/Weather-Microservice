using BuildingBlock.Base.Models.Base;

namespace Services.DataProcessService.Aggregate.ValueObjects
{
    public sealed class CurrentWeatherId : ValueObject
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public CurrentWeatherId(Guid id)
        {
            Id = id;
        }

        public static CurrentWeatherId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static CurrentWeatherId Create(Guid Id)
        {
            return new CurrentWeatherId(Id);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
