using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

namespace ApiGateway.Api.Registrations
{
    public static class ConsulRegistration
    {
        public static IServiceCollection ConsulServiceRegistration(this IServiceCollection services)
        {
            services.AddOcelot().AddConsul();

            return services;
        }

        public static IApplicationBuilder ConsulApplicationBuilderRegsitration(this IApplicationBuilder app, IHostApplicationLifetime lifetime, IConfiguration configuration)
        {
            app.UseOcelot().GetAwaiter().GetResult();

            return app;
        }
    }
}
