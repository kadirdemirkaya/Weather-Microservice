using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Models.Base;
using Services.UserInfoService.Aggregates.Enums;
using Services.UserInfoService.Aggregates.ValueObjects;

namespace Services.UserInfoService.Aggregates.Entities
{
    public class RoleUser : Entity<RoleUserId>
    {
        public Role Role { get; set; }
        public RoleId RoleId { get; private set; }

        public User User { get; set; }
        public UserId UserId { get; private set; }

        public UserRoleStatus UserRoleStatus { get; private set; }
        public bool IsActive { get; private set; } = true;


        public RoleUser()
        {

        }
        public RoleUser(RoleUserId Id) : base(Id)
        {

        }
        public RoleUser(RoleUserId Id, RoleId roleId, UserId userId, bool isActive, UserRoleStatus userRoleStatus) : base(Id)
        {
            RoleId = roleId;
            UserId = userId;
            UserRoleStatus = userRoleStatus;
            IsActive = isActive;
        }

        public static RoleUser Create(RoleId roleId, UserId userId, bool isActive, UserRoleStatus userRoleStatus)
            => new(RoleUserId.CreateUnique(), roleId, userId, isActive, userRoleStatus);
        public static RoleUser Create(Guid Id, RoleId roleId, UserId userId, bool isActive, UserRoleStatus userRoleStatus)
            => new(RoleUserId.Create(Id), roleId, userId, isActive, userRoleStatus);
        public static RoleUser Create(RoleUserId Id, RoleId roleId, UserId userId, bool isActive, UserRoleStatus userRoleStatus)
            => new(Id, roleId, userId, isActive, userRoleStatus);

        public void AddUserDomainEvent(IDomainEvent @event)
        {
            AddDomainEvent(@event);
        }

        public void CountDomainEvent() => DomainEvents.Count();
    }
}
