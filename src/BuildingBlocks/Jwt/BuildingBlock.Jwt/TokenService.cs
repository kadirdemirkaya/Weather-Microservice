using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Models;
using BuildingBlock.Dapper;
using Dapper;
using Elastic.CommonSchema;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace BuildingBlock.Jwt
{
    public class TokenService : ITokenService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IDapperService<User, UserId> _dapperService;

        public TokenService(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor, DatabaseConfig config, DbContext context)
        {
            _serviceProvider = serviceProvider;
            _unitOfWork = GetUnitOfWorkService();
            _httpContextAccessor = GetHttpContextService();
            //_dapperService = new DapperService<User, UserId>(config, context, serviceProvider);
        }
        public TokenService(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
        {
            _serviceProvider = serviceProvider;
            _unitOfWork = GetUnitOfWorkService();
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetEmailWithToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadJwtToken(token);
            var emailClaim = tokenS.Claims.FirstOrDefault(c => c.Type == "email");
            if (emailClaim != null)
            {
                string email = emailClaim.Value;
                return email;
            }
            return string.Empty;
        }

        public bool TokenIsValid(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken? jsonToken = handler.ReadToken(token);
            DateTime? expireDate = jsonToken.ValidTo;
            return DateTime.Now > expireDate ? false : true;
        }

        public async Task<UserModel> GetUserWithTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var readToken = tokenHandler.ReadJwtToken(token);

            var emailClaim = readToken.Claims.FirstOrDefault(claim => claim.Type == "email");

            if (emailClaim != null)
            {
                #region procedure get 
                //var user = await _dapperService.GetEntityStoredProcedure(BuildingBlock.Dapper.Constant.ProcedureNames.GetUserByEmail, GetDynamicParametersForUser(emailClaim.Value));

                //return new() { Email = user.Email, FullName = user.FullName, Id = user.Id.Id };
                #endregion

                throw new NotImplementedException("GetUserWithTokenAsync() not implemented");
            }
            else
                return null;
        }
        private IUnitOfWork GetUnitOfWorkService()
         => _serviceProvider.GetRequiredService<IUnitOfWork>();

        public string GetTokenInHeader()
        {
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (token != null && token.StartsWith("Bearer "))
                return token.Substring("Bearer ".Length).Trim();
            return null;
        }

        private IHttpContextAccessor GetHttpContextService()
            => _serviceProvider.GetRequiredService<IHttpContextAccessor>();

        private DbContext GetDbContextService()
            => _serviceProvider.GetRequiredService<DbContext>();

        private DynamicParameters GetDynamicParametersForUser(string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email, System.Data.DbType.String);
            return parameters;
        }
    }




    public class TokenService<T> : ITokenService<T>
        where T : class
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDapperService<User> _dapperService;

        public TokenService(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor, DatabaseConfig config, DbContext context)
        {
            _serviceProvider = serviceProvider;
            _unitOfWork = GetUnitOfWorkService();
            _httpContextAccessor = GetHttpContextService();
            _dapperService = new DapperService<User>(config, context, serviceProvider);
        }
        public TokenService(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
        {
            _serviceProvider = serviceProvider;
            _unitOfWork = GetUnitOfWorkService();
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetEmailWithToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadJwtToken(token);
            var emailClaim = tokenS.Claims.FirstOrDefault(c => c.Type == "email");
            if (emailClaim != null)
            {
                string email = emailClaim.Value;
                return email;
            }
            return string.Empty;
        }

        public bool TokenIsValid(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken? jsonToken = handler.ReadToken(token);
            DateTime? expireDate = jsonToken.ValidTo;
            return DateTime.Now > expireDate ? false : true;
        }

        public async Task<UserModel> GetUserWithTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var readToken = tokenHandler.ReadJwtToken(token);

            var emailClaim = readToken.Claims.FirstOrDefault(claim => claim.Type == "email");

            if (emailClaim != null)
            {
                #region procedure get
                //var user = await _dapperService.GetEntityStoredProcedure(BuildingBlock.Dapper.Constant.ProcedureNames.GetUserByEmail, GetDynamicParametersForUser(emailClaim.Value));

                //return new() { Email = user.Email, FullName = user.FullName, Id = user.Id.Id };
                #endregion
                throw new NotImplementedException("GetUserWithTokenAsync() not implemented");
            }
            else
                return null;
        }
        private IUnitOfWork GetUnitOfWorkService()
         => _serviceProvider.GetRequiredService<IUnitOfWork>();

        public string GetTokenInHeader()
        {
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (token != null && token.StartsWith("Bearer "))
                return token.Substring("Bearer ".Length).Trim();
            return null;
        }

        private IHttpContextAccessor GetHttpContextService()
            => _serviceProvider.GetRequiredService<IHttpContextAccessor>();

        private DbContext GetDbContextService()
            => _serviceProvider.GetRequiredService<DbContext>();

        private DynamicParameters GetDynamicParametersForUser(string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email, System.Data.DbType.String);
            return parameters;
        }
    }
}
