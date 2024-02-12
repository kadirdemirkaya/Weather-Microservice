using MediatR;
using Services.ClientAndServerService.Abstractions;

namespace Services.ClientAndServerService.Features.User.Commands.UserLogout
{
    public class UserLogoutCommandHandler : IRequestHandler<UserLogoutCommandRequest, UserLogoutCommandResponse>
    {
        private readonly IUserService _userService;


        public UserLogoutCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserLogoutCommandResponse> Handle(UserLogoutCommandRequest request, CancellationToken cancellationToken)
            => new(await _userService.UserLogoutAsync(request.token));
    }
}
