using BuildingBlock.Base.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Services.LocationService.Seeds;

namespace Services.LocationService.Registrations
{
    public static class SeedRegistration
    {
        public static IServiceCollection SeedServiceRegistration(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();

            var unitOfWork = sp.GetRequiredService<IUnitOfWork>();
            LocationContextSeed locationContextSeed = new(unitOfWork);
            locationContextSeed.SeedAsync().GetAwaiter();

            return services;
        }
    }
}
