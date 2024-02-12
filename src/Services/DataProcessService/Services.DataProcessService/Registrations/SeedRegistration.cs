using Microsoft.Extensions.DependencyInjection;
using Services.DataProcessService.Data;
using Services.DataProcessService.Seeds;

namespace Services.DataProcessService.Registrations
{
    public static class SeedRegistration
    {
        public static IServiceCollection SeedServiceRegistration(this IServiceCollection services)
        {
            var dbContextSeed = new DataProcessContextSeed();
            var sp = services.BuildServiceProvider();
            var context = sp.GetRequiredService<WeatherDbContext>();

            dbContextSeed.SeedAsync(context).GetAwaiter();

            return services;
        }
    }
}
