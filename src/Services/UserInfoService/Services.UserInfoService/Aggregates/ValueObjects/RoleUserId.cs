using BuildingBlock.Base.Models.Base;

namespace Services.UserInfoService.Aggregates.ValueObjects
{
    public sealed class RoleUserId : ValueObject
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public RoleUserId(Guid id)
        {
            Id = id;
        }

        public static RoleUserId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static RoleUserId Create(Guid Id)
        {
            return new RoleUserId(Id);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
