using BuildingBlock.Base.Models.Base;

namespace Services.UserInfoService.Aggregates.ValueObjects
{
    public sealed class RoleId : ValueObject
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public RoleId(Guid id)
        {
            Id = id;
        }

        public static RoleId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static RoleId Create(Guid Id)
        {
            return new RoleId(Id);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
