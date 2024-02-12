using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Models.Base;
using Services.UserInfoService.Aggregates.Entities;
using Services.UserInfoService.Aggregates.Enums;
using Services.UserInfoService.Aggregates.ValueObjects;

namespace Services.UserInfoService.Aggregates
{
    public class User : AggregateRoot<UserId>
    {
        public string Email { get; private set; }
        public string Password { get; private set; }
        public UserStatus UserStatus { get; private set; } = UserStatus.Active;

        public DateTime CreatedDate { get; private set; } = DateTime.Now;

        public string? RefreshToken { get; private set; }
        public DateTime? RefreshTokenEndDate { get; private set; }


        private readonly List<RoleUser> _roleUsers = new();
        public IReadOnlyCollection<RoleUser> RoleUsers => _roleUsers.AsReadOnly();



        public User()
        {

        }
        public User(UserId Id) : base(Id)
        {

        }
        public User(UserId id, string email, string password, DateTime createdDate, UserStatus userStatus) : base(id)
        {
            Email = email;
            Password = password;
            CreatedDate = createdDate;
            UserStatus = userStatus;
        }

        public static User Create(string email, string password, DateTime createdDate, UserStatus userStatus)
        {
            return new(UserId.CreateUnique(), email, password, createdDate, userStatus);
        }

        public static User Create(Guid id, string email, string password, DateTime createdDate, UserStatus userStatus)
        {
            return new(UserId.Create(id), email, password, createdDate, userStatus);
        }

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

        public void UpdateTokenProperties(string refreshToken, DateTime refreshTokenEndDate)
        {
            RefreshToken = refreshToken;
            RefreshTokenEndDate = refreshTokenEndDate;
        }
    }
}
