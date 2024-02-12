using AutoMapper;
using Grpc.Core;
using MediatR;
using Services.UserInfoService.Dtos;
using Services.UserInfoService.Features.Commands.UserLogin;
using Services.UserInfoService.Features.Commands.UserLogout;
using Services.UserInfoService.Features.Commands.UserRegister;
using UserInfoService;

namespace Services.UserInfoService.Services.Grpc
{
    public class UserService : GrpcUser.GrpcUserBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UserService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<UserLoginModelResponse> UserLogin(UserLoginModelRequest request, ServerCallContext context)
        {
            UserLoginCommandRequest userLoginCommandRequest = new(_mapper.Map<UserLoginDto>(request.UserLoginModel));
            UserLoginCommandResponse userLoginCommandResponse = await _mediator.Send(userLoginCommandRequest);
            return new() { Issuccess = userLoginCommandResponse.IsSuccess, Token = userLoginCommandResponse.Token };
        }

        public override async Task<UserLogoutModelResponse> UserLogout(UserLogoutModelRequest request, ServerCallContext context)
        {
            UserLogoutCommandRequest userLogoutCommandRequest = new(request.UserLogoutModel.Token);
            UserLogoutCommandResponse userLoginCommandResponse = await _mediator.Send(userLogoutCommandRequest);
            return new() { Response = userLoginCommandResponse.response };
        }

        public override async Task<UserRegisterModelResponse> UserRegister(UserRegisterModelRequest request, ServerCallContext context)
        {
            UserRegisterCommandRequest userRegisterCommandRequest = new(_mapper.Map<UserRegisterDto>(request.UserRegisterModel));
            UserRegisterCommandResponse userRegisterCommandResponse = await _mediator.Send(userRegisterCommandRequest);
            return new() { Response = userRegisterCommandResponse.response };
        }
    }
}
