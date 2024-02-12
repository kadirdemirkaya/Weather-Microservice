using BuildingBlock.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Services.LocationService.Registrations
{
    public static class LoggerRegistration
    {
        public static WebApplicationBuilder LoggerServiceRegistration(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.AddFileExtensionBuilder(configuration);

            return builder;
        }
    }
}
