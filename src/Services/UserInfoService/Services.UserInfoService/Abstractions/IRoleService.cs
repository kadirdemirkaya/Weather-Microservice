using Services.UserInfoService.Aggregates.Entities;
using Services.UserInfoService.Aggregates.ValueObjects;

namespace Services.UserInfoService.Abstractions
{
    public interface IRoleService
    {
        Task<Role> GetUserRole(UserId Id);

        Task<List<Role>> GetUserRoles(UserId Id);
    }
}
