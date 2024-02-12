using BuildingBlock.Base.Extensions;
using System.Formats.Tar;

namespace Services.DataProcessService.Api.Registrations
{
    public static class SessionRegistration
    {
        public static IServiceCollection SessionServiceRegistration(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var sessionOpt = services.GetOptions<BuildingBlock.Base.Options.SessionOptions>(nameof(BuildingBlock.Base.Options.SessionOptions));

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(sessionOpt.ExpiryMinutes);
            });

            return services;
        }

        public static WebApplication SessionApplicationRegistration(this WebApplication app)
        {
            app.UseSession();

            return app;
        }
    }
}
