using Microsoft.AspNetCore.Builder;
using Services.UserInfoService.Middlewares;

namespace Services.UserInfoService.Registrations
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
