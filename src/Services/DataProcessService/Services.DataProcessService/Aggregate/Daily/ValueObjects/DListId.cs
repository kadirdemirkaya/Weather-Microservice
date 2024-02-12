using BuildingBlock.Base.Models.Base;

namespace Services.DataProcessService.Aggregate.Daily.ValueObjects
{
    public sealed class DListId : ValueObject
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public DListId(Guid id)
        {
            Id = id;
        }

        public static DListId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static DListId Create(Guid Id)
        {
            return new DListId(Id);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
