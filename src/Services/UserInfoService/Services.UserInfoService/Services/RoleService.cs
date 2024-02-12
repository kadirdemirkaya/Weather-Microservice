using BuildingBlock.Base.Abstractions;
using Services.UserInfoService.Abstractions;
using Services.UserInfoService.Aggregates.Entities;
using Services.UserInfoService.Aggregates.ValueObjects;

namespace Services.UserInfoService.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private List<Role> Roles;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Roles = new();
        }

        public async Task<Role> GetUserRole(UserId Id)
        {
            //var role = await _unitOfWork.GetReadRepository<RoleUser, RoleUserId>().GetAsync(r => r.UserId == Id);
            //return await _unitOfWork.GetReadRepository<Role, RoleId>().GetAsync(r => r.Id == role.RoleId);
            var role = await _unitOfWork.GetReadRepository<RoleUser, RoleUserId>().GetAsync(ru => ru.UserId == Id, true, ru => ru.Role);
            return role.Role;
        }

        public async Task<List<Role>> GetUserRoles(UserId Id)
        {
            var roles = await _unitOfWork.GetReadRepository<RoleUser, RoleUserId>().GetAllAsync(r => r.UserId == Id);
            foreach (var role in roles)
            {
                Roles.Add(await _unitOfWork.GetReadRepository<Role, RoleId>().GetAsync(r => r.Id == role.RoleId));
            }
            return Roles;
        }
    }
}
