using BuildingBlock.Base.Extensions;
using BuildingBlock.Base.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Services.DataProcessService.Data;

namespace Services.DataProcessService.Registrations
{
    public static class DatabaseRegistration
    {
        public static IServiceCollection DatabaseServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            var dbOptions = services.GetOptions<DatabaseOptions>(nameof(DatabaseOptions));

            services.AddDbContext<WeatherDbContext>(options => options.UseNpgsql(dbOptions.ConnectionUrl));

            return services;
        }
    }
}
