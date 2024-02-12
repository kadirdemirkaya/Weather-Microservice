namespace Services.ClientAndServerService.Api.Registrations
{
    public static class ControllerRegistration
    {
        public static IServiceCollection ControllerServiceRegistration(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }

        public static WebApplication ControllerApplicationRegistration(this WebApplication app)
        {
            app.MapControllers();

            return app;
        }
    }
}
