using BuildingBlock.Logger;

namespace ApiGateway.Api.Registrations
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
