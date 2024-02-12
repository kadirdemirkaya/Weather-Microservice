using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Models;
using Microsoft.Extensions.Configuration;
using Services.UserInfoService.Abstractions;
using Services.UserInfoService.Aggregates;
using Services.UserInfoService.Aggregates.ValueObjects;

namespace Services.UserInfoService.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator, IConfiguration configuration, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<UserModel?> ConfirmByEmailAsync(string token, string email)
        {
            string _email = _tokenService.GetEmailWithToken(token);
            if (_email.Equals(email))
            {
                User? user = await _unitOfWork.GetReadRepository<User, UserId>().GetAsync(u => u.Email == _email);
                return new() { Email = user.Email, Id = user.Id.Id.ToString() };
            }
            return null;
        }

        public async Task<UserModel?> GetUserByEmailAsync(string email)
        {
            User user = await _unitOfWork.GetReadRepository<User, UserId>().GetAsync(u => u.Email == email);
            return new() { Email = user.Email, Id = user.Id.Id.ToString() };
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            User? user = await _unitOfWork.GetReadRepository<User, UserId>().GetAsync(u => u.RefreshToken == refreshToken);
            if (user is not null && user?.RefreshTokenEndDate > DateTime.Now)
            {
                Token token = _jwtTokenGenerator.GenerateToken(new UserModel() { Email = user.Email, Id = user.Id.Id.ToString() });
                await UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, int.Parse(_configuration["JwtSettings:ExpireMinuteRefToken"]));
                return token;
            }
            return null;
        }

        public async Task<bool> ResetPasswordAsync(UserModel userModel, string token, string password)
        {
            //to do
            return default;
        }

        public async Task<bool> UpdateRefreshTokenAsync(string refreshToken, User user, DateTime accessTokenDate, int refreshTokenLifeTimeSecond)
        {
            if (user is not null)
            {
                user.UpdateTokenProperties(refreshToken, accessTokenDate.AddMinutes(refreshTokenLifeTimeSecond));

                _unitOfWork.GetWriteRepository<User, UserId>().UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
