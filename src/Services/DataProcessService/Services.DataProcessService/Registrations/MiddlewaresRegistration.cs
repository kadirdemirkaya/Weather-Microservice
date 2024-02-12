using Microsoft.AspNetCore.Builder;
using Services.DataProcessService.Middlewares;

namespace Services.DataProcessService.Registrations
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
