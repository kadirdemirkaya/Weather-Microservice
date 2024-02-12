using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BuildingBlock.Logger;

namespace BuildingBlock.Email
{
    public static class Extension
    {
        public static IServiceCollection EmailServiceExtension(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }

        public static WebApplicationBuilder EmailBuilderExtension(this WebApplicationBuilder builder,IConfiguration configuration)
        {
            builder.AddFileExtensionBuilder(configuration);

            return builder;
        }
    }
}
