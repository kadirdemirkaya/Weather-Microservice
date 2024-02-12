using Microsoft.AspNetCore.Builder;
using Services.ClientAndServerService.Middlewares;

namespace Services.ClientAndServerService.Registrations
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
