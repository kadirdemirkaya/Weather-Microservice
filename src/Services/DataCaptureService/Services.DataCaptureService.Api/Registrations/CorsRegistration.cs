﻿namespace Services.DataCaptureService.Api.Registrations
{
    public static class CorsRegistration
    {
        public static IServiceCollection CorsServiceRegistration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder
                           .WithOrigins("https://localhost:7282", "https://localhost:7247", "http://localhost:5154", "http://localhost:5004")
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
            app.UseCors("AllowSpecificOrigin");

            return app;
        }
    }
}
