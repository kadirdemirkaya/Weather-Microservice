using BuildingBlock.Base.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Services.LocationService.Setups;

namespace Services.LocationService.Registrations
{
    public static class SetupRegistration
    {
        public static IServiceCollection SetupServiceRegistration(this IServiceCollection services)
        {
            services.AddSingleton<IConfigureOptions<FakeOptions>, FakeOptionsSetup>();
            services.AddSingleton<IConfigureOptions<EventBusOptions>, EventBusOptionsSetup>();

            return services;
        }
    }
}
