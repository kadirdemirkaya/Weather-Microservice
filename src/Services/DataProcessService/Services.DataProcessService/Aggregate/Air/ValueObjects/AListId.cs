using BuildingBlock.Base.Models.Base;

namespace Services.DataProcessService.Aggregate.Air.ValueObjects
{
    public sealed class AListId : ValueObject
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public AListId(Guid id)
        {
            Id = id;
        }

        public static AListId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static AListId Create(Guid Id)
        {
            return new AListId(Id);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
