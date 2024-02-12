using BuildingBlock.Base.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BuildingBlock.Logger;

namespace BuildingBlock.Redis
{
    namespace EventBusRedis
    {
        public static class Extension
        {
            public static IServiceCollection RedisServiceExtension(this IServiceCollection services, IConfiguration configuration)
            {
                services.AddScoped(typeof(IRedisService<>), typeof(RedisService<>));

                services.AddScoped(typeof(IRedisService<,>), typeof(RedisService<,>));

                services.AddScoped(typeof(IRedisRepository<,>), typeof(RedisRepository<,>));

                return services;
            }

            public static WebApplicationBuilder RedisBuilderExtension(this WebApplicationBuilder builder, IConfiguration configuration)
            {
                builder.AddFileExtensionBuilder(configuration);

                return builder;
            }
        }
    }
}
