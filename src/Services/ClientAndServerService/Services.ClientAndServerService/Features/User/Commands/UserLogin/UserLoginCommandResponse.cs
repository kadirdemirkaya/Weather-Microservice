using Services.ClientAndServerService.Models;

namespace Services.ClientAndServerService.Features.User.Commands.UserLogin
{
    public record UserLoginCommandResponse (
        UserLoginResponseModel UserLoginResponseModel
    );
}
