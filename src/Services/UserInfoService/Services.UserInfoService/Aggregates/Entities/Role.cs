using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Models.Base;
using Services.UserInfoService.Aggregates.Enums;
using Services.UserInfoService.Aggregates.ValueObjects;

namespace Services.UserInfoService.Aggregates.Entities
{
    public class Role : Entity<RoleId>
    {
        public RoleEnum RoleEnum { get; private set; }
        public bool IsActive { get; private set; }


        private readonly List<RoleUser> _roleUsers = new();
        public IReadOnlyCollection<RoleUser> RoleUsers => _roleUsers.AsReadOnly();

        public Role()
        {

        }

        public Role(RoleId Id, RoleEnum roleEnum, bool isActive) : base(Id)
        {
            RoleEnum = roleEnum;
            IsActive = isActive;
        }


        public static Role Create(RoleEnum roleEnum, bool isActive)
            => new(RoleId.CreateUnique(), roleEnum, isActive);

        public static Role Create(Guid Id, RoleEnum roleEnum, bool isActive)
            => new(RoleId.Create(Id), roleEnum, isActive);

        public static Role Create(RoleId Id, RoleEnum roleEnum, bool isActive)
            => new(Id, roleEnum, isActive);

        public void AddUserRole(RoleId roleId, UserId userId, bool isActive, UserRoleStatus userRoleStatus)
        {
            _roleUsers.Add(RoleUser.Create(roleId, userId, isActive, userRoleStatus));
        }

        public List<RoleUser> GetUserRoles() => RoleUsers.ToList();

        public void AddUserDomainEvent(IDomainEvent @event)
        {
            AddDomainEvent(@event);
        }

        public void CountDomainEvent() => DomainEvents.Count();
    }
}
