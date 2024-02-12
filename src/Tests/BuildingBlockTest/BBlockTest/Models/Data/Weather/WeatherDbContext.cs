using BBlockTest.Constants;
using DnsClient.Protocol.Options;
using Microsoft.EntityFrameworkCore;

namespace BBlockTest.Models.Data.Weather
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
        {
        }

        public WeatherDbContext()
        {
        }

        public DbSet<BBlockTest.Aggregate.Weather.Weather> Weathers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Constant.Postgre.PostgreDbUrl);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
