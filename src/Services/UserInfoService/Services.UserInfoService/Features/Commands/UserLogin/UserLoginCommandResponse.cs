namespace Services.UserInfoService.Features.Commands.UserLogin
{
    public record UserLoginCommandResponse(
        bool IsSuccess,
        string Token
    );
}
