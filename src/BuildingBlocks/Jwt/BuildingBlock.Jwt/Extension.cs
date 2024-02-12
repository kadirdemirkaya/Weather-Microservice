using BuildingBlock.Base.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Jwt
{
    public static class Extension
    {
        public static IServiceCollection JwtServiceExtension(this IServiceCollection services)
        {
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
