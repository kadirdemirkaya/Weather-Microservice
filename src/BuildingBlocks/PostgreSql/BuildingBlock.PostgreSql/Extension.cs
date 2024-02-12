using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.PostgreSql
{
    public static class Extension
    {
        public static IServiceCollection PostgreSqlServiceExtension(this IServiceCollection services,IConfiguration configuration)
        {

            return services;
        }

        public static WebApplicationBuilder PostgreSqlBuilderExtension(this WebApplicationBuilder builder,IConfiguration configuration)
        {

            return builder;
        }
    }
}
