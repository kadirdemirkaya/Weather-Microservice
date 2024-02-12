using Microsoft.AspNetCore.Builder;
using Services.LocationService.Middlewares;

namespace Services.LocationService.Registrations
{
    public static class MiddlewaresRegistration
    {
        public static WebApplication MiddlewaresApplicationRegistration(this WebApplication app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseMiddleware<JwtBlacklistMiddleware>();

            return app;
        }
    }
}
