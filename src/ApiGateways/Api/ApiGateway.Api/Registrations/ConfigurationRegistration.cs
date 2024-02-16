namespace ApiGateway.Api.Registrations
{
    public static class ConfigurationRegistration
    {
        public static WebApplicationBuilder ConfigurationBuilderRegistration(this WebApplicationBuilder builder)
        {
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Configurations/ocelot.json")
                .AddEnvironmentVariables();

            return builder;
        }
    }
}
