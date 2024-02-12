using Services.ClientAndServerService.Models;

namespace Services.ClientAndServerService.Abstractions
{
    public interface IUserService
    {
        Task<Models.UserLoginResponseModel> UserLoginAsync(Models.UserLoginModel userLoginModel);

        Task<bool> UserLogoutAsync(string token);

        Task<bool> UserRegisterAsync(Models.UserRegisterModel userRegisterModel);
    }
}
