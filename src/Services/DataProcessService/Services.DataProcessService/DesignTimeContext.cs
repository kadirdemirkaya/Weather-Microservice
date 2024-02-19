using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Services.DataProcessService.Configurations.Configs;
using Services.DataProcessService.Data;

namespace Services.DataProcessService
{
    public class DesignTimeContext2 : IDesignTimeDbContextFactory<WeatherDbContext>
    {
        public WeatherDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<WeatherDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(GetConfigs.GetDatabaseConfig().ConnectionString.ToString());
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
