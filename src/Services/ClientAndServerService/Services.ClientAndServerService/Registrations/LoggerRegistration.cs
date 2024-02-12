using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using BuildingBlock.Logger;

namespace Services.ClientAndServerService.Registrations
{
    public static class LoggerRegistration
    {
        public static WebApplicationBuilder LoggerBuilderRegistration(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.AddFileExtensionBuilder(configuration);

            return builder;
        }
    }
}
