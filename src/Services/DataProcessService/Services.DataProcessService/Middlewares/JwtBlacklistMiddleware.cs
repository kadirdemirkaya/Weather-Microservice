using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Services.DataProcessService.Middlewares
{
    public class JwtBlacklistMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenBlacklistService _tokenBlacklistService;

        public JwtBlacklistMiddleware(RequestDelegate next, ITokenBlacklistService tokenBlacklistService)
        {
            _next = next;
            _tokenBlacklistService = tokenBlacklistService;
        }

        public async Task Invoke(HttpContext context)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null && _tokenBlacklistService.IsTokenBlacklisted(token))
                throw new JwtErrorException("Token is blacklisted");

            await _next(context);
        }
    }
}
