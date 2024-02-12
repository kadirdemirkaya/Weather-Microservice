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
            dbContextOptionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=WeatherDbContext;User Id=postgres;Password=123");
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
