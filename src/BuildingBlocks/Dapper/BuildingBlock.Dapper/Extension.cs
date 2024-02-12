using BuildingBlock.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Dapper
{
    public static class Extension
    {
        public static IServiceCollection DapperServiceExtension(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }

        public static WebApplicationBuilder DapperApplicationBuilderExtension(this WebApplicationBuilder builder, IConfiguration configuration)
        {

            builder.AddFileExtensionBuilder(configuration);

            return builder;
        }
    }
}