using BuildingBlock.Base.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Services.UserInfoService.Features.Commands.UserLogout
{
    public class UserLogoutCommandHandler : IRequestHandler<UserLogoutCommandRequest, UserLogoutCommandResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenBlacklistService _tokenBlacklistService;

        public UserLogoutCommandHandler(IHttpContextAccessor httpContextAccessor, ITokenBlacklistService tokenBlacklistService)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenBlacklistService = tokenBlacklistService;
        }

        public async Task<UserLogoutCommandResponse> Handle(UserLogoutCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                string token = _httpContextAccessor.HttpContext.Session.GetString(Constants.Constant.Keys.UserToken);
                _tokenBlacklistService.AddToBlacklist(token);
                _httpContextAccessor.HttpContext.Session.Remove(Constants.Constant.Keys.UserEmail);
                _httpContextAccessor.HttpContext.Session.Remove(Constants.Constant.Keys.UserRole);
                _httpContextAccessor.HttpContext.Session.Remove(Constants.Constant.Keys.UserToken);
                return new(true);
            }
            catch (Exception ex)
            {
                return new(false);
            }
        }
    }
}
