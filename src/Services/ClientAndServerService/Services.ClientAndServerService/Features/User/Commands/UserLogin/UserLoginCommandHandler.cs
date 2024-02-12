using AutoMapper;
using MediatR;
using Services.ClientAndServerService.Abstractions;
using Services.ClientAndServerService.Models;

namespace Services.ClientAndServerService.Features.User.Commands.UserLogin
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommandRequest, UserLoginCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserLoginCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UserLoginCommandResponse> Handle(UserLoginCommandRequest request, CancellationToken cancellationToken)
            => new(await _userService.UserLoginAsync(_mapper.Map<UserLoginModel>(request.UserLoginModelDto)));
    }
}
