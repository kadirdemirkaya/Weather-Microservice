using MediatR;

namespace Services.ClientAndServerService.Features.User.Commands.UserLogout
{
    public record UserLogoutCommandRequest (
        string token
    ) : IRequest<UserLogoutCommandResponse>;
}
