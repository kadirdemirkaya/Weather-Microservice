using MediatR;
using Services.ClientAndServerService.Dtos;

namespace Services.ClientAndServerService.Features.User.Commands.UserLogin
{
    public record UserLoginCommandRequest(
        UserLoginModelDto UserLoginModelDto
    ) : IRequest<UserLoginCommandResponse>;
}
