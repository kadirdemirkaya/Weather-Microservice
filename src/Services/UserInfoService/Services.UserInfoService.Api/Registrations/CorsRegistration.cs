namespace Services.UserInfoService.Api.Registrations
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
                        builder
                           .WithOrigins("https://localhost:7282", "http://localhost:5154")
                           .AllowAnyHeader()
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
