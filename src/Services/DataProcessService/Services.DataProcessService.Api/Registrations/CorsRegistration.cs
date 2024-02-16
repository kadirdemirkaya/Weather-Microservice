using MongoDB.Bson.Serialization.Conventions;

namespace Services.DataProcessService.Api.Registrations
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
                           .WithOrigins("https://localhost:7282", "https://localhost:7252", "http://localhost:5154", "http://localhost:5002")
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
