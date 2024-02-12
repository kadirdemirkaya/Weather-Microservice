using BuildingBlock.Base.Models.Base;

namespace Services.DataProcessService.Aggregate.Current.ValueObjects
{
    public sealed class WeatherId : ValueObject
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public WeatherId(Guid id)
        {
            Id = id;
        }

        public static WeatherId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static WeatherId Create(Guid Id)
        {
            return new WeatherId(Id);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
