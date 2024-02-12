using MediatR;
using Services.ClientAndServerService.Dtos;

namespace Services.ClientAndServerService.Features.User.Commands.UserRegister
{
    public record UserRegisterCommandRequest(
        UserRegisterModelDto UserRegisterModelDto
    ) : IRequest<UserRegisterCommandResponse>;
}
