using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Services.DataProcessService.Data;

namespace Services.DataProcessService
{
    public class DesignTimeContext2 : IDesignTimeDbContextFactory<WeatherDbContext>
    {
        public WeatherDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<WeatherDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql("Server=postgresql-clusterip-service;port=5433;Database=WeatherDbContext;User Id=postgresql;Password=123");
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
