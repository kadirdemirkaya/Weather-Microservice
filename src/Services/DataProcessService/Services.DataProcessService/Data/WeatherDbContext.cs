using BuildingBlock.Base.Abstractions;
using Microsoft.EntityFrameworkCore;
using Services.DataProcessService.Aggregate;
using Services.DataProcessService.Aggregate.Air;
using Services.DataProcessService.Aggregate.Air.Entities;
using Services.DataProcessService.Aggregate.Current.Entities;
using Services.DataProcessService.Aggregate.Daily;
using Services.DataProcessService.Aggregate.Daily.Entities;
using Services.DataProcessService.Configurations.Configs;

namespace Services.DataProcessService.Data
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext()
        {
        }

        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
        {
        }

        public DbSet<CurrentWeather> CurrentWeathers { get; set; }
        public DbSet<CWeather> CWeathers { get; set; }
        public DbSet<DailyWeather> DailyWeathers { get; set; }
        public DbSet<DWeather> DWeathers { get; set; }
        public DbSet<DList> Lists { get; set; }
        public DbSet<AirPollutionWeather> AirPollutionWeathers { get; set; }
        public DbSet<AList> ALists { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(GetConfigs.GetDatabaseConfig().ConnectionString.ToString());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<List<IDomainEvent>>();
            modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
