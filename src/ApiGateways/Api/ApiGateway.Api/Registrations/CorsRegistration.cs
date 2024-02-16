namespace ApiGateway.Api.Registrations
{
    public static class CorsRegistration
    {
        public static IServiceCollection CorsServiceRegistration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder =>
                    {
                        builder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .SetIsOriginAllowed(_ => true)
                           .AllowCredentials();
                    });
            });

            return services;
        }

        public static WebApplication CorsApplicationRegistration(this WebApplication app)
        {
            app.UseCors("AllowOrigin");

            return app;
        }
    }
}
