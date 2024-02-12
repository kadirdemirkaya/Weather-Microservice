using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Exceptions;
using BuildingBlock.Base.Extensions;
using BuildingBlock.Base.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Services.UserInfoService.Abstractions;
using Services.UserInfoService.Aggregates;
using Services.UserInfoService.Aggregates.Entities;
using Services.UserInfoService.Aggregates.ValueObjects;
using Services.UserInfoService.Configurations.Configs;

namespace Services.UserInfoService.Features.Commands.UserLogin
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommandRequest, UserLoginCommandResponse>
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserLoginCommandHandler(IUnitOfWork unitOfWork, IRoleService roleService, IUserService userService, IConfiguration configuration, IJwtTokenGenerator jwtTokenGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _roleService = roleService;
            _userService = userService;
            _configuration = configuration;
            _jwtTokenGenerator = jwtTokenGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserLoginCommandResponse> Handle(UserLoginCommandRequest request, CancellationToken cancellationToken)
        {
            request.userLoginDto.Password = PasswordHashExtension.StringHashingEncrypt(request.userLoginDto.Password, GetConfigs.GetEncryptionKey());

            User? user = await _unitOfWork.GetReadRepository<User, UserId>().GetAsync(u => u.Email == request.userLoginDto.Email && u.Password == request.userLoginDto.Password);

            if (user is null)
                throw new ValueNullErrorException(nameof(User), "This information does not enable login !");

            Role? role = await _roleService.GetUserRole(user.Id);

            Token? token = _jwtTokenGenerator.GenerateTokenWithRole(new() { Email = user.Email, Id = user.Id.Id.ToString() }, role.RoleEnum.ToString());

            if (token is null)
                throw new ServiceErrorException("Token value is null !");

            bool servResult = await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, int.Parse(_configuration["JwtOptions:ExpireMinuteRefToken"]));

            if (servResult is false)
                throw new ServiceErrorException(nameof(IUserService), "token refresh error");

            _httpContextAccessor.HttpContext.Session.SetString(Constants.Constant.Keys.UserEmail, user.Email);
            _httpContextAccessor.HttpContext.Session.SetString(Constants.Constant.Keys.UserToken, token.AccessToken);
            _httpContextAccessor.HttpContext.Session.SetString(Constants.Constant.Keys.UserRole, role.RoleEnum.ToString());

            return new(true, token.AccessToken);
        }
    }
}
