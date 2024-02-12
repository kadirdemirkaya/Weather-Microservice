using BuildingBlock.Base.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BuildingBlock.Base.Abstractions
{
    public interface IJwtTokenGenerator
    {
        JwtSecurityToken GetToken(List<Claim> authClaims);

        Token GenerateToken(UserModel user);
        Token GenerateTokenWithRole(UserModel user, string role);
    }
}
