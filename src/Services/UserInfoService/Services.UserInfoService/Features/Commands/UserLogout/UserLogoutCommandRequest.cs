using MediatR;

namespace Services.UserInfoService.Features.Commands.UserLogout
{
    public record UserLogoutCommandRequest(
        string token
    ) : IRequest<UserLogoutCommandResponse>;
}
