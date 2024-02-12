using BuildingBlock.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Services.UserInfoService.Registrations
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
