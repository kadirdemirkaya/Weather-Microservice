using MediatR;
using Services.UserInfoService.Dtos;

namespace Services.UserInfoService.Features.Commands.UserRegister
{
    public record UserRegisterCommandRequest (
        UserRegisterDto userRegisterDto
    ) : IRequest<UserRegisterCommandResponse>;
}
