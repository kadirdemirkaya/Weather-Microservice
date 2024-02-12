using AutoMapper;
using MediatR;
using Services.ClientAndServerService.Abstractions;
using Services.ClientAndServerService.Models;

namespace Services.ClientAndServerService.Features.User.Commands.UserRegister
{
    public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommandRequest, UserRegisterCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserRegisterCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UserRegisterCommandResponse> Handle(UserRegisterCommandRequest request, CancellationToken cancellationToken)
            => new(await _userService.UserRegisterAsync(_mapper.Map<UserRegisterModel>(request.UserRegisterModelDto)));
    }
}
