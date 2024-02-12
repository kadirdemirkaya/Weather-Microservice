using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Extensions;
using BuildingBlock.Base.Options;
using BuildingBlock.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.UserInfoService.Configurations.Configs;
using Services.UserInfoService.Data;

namespace Services.UserInfoService.Registrations
{
    public static class JsonWebTokenRegistration
    {
        public static IServiceCollection JsonWebTokenServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            var options = services.GetOptions<JwtOptions>(nameof(JwtOptions));

            services.AddScoped<IJwtTokenGenerator>(sp =>
            {
                return new JwtTokenGenerator(options);
            });

            var sp = services.BuildServiceProvider();

            var httpContextAccesor = sp.GetRequiredService<IHttpContextAccessor>();

            var context = sp.GetRequiredService<UserDbContext>();

            services.AddScoped<ITokenService>(sp =>
            {
                return new TokenService(sp, httpContextAccesor, GetConfigs.GetDatabaseConfig(), context);
            });

            return services;
        }
    }
}
