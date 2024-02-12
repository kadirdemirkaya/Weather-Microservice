using MediatR;
using Services.UserInfoService.Dtos;

namespace Services.UserInfoService.Features.Commands.UserLogin
{
    public record UserLoginCommandRequest (
        UserLoginDto userLoginDto
    ) : IRequest<UserLoginCommandResponse>;
}
