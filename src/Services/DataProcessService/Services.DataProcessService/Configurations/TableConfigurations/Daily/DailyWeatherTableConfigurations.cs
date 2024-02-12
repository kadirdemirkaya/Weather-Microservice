using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.DataProcessService.Aggregate.Daily;
using Services.DataProcessService.Aggregate.Daily.ValueObjects;
using static Services.DataProcessService.Constants.Constant;

namespace Services.DataProcessService.Configurations.TableConfigurations.Daily
{
    public class DailyWeatherTableConfigurations : IEntityTypeConfiguration<Aggregate.Daily.DailyWeather>
    {
        public void Configure(EntityTypeBuilder<DailyWeather> builder)
        {
            builder.ToTable(Tables.DailyWeathers);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
              .ValueGeneratedNever()
              .HasConversion(
                  id => id.Id,
                  value => DailyWeatherId.Create(value));

            builder.Property(d => d.Cnt);

            builder.Property(d => d.Cod);

            builder.Property(d => d.Message);

            builder.OwnsOne(d => d.City, dd =>
            {
                dd.Property(d => d.Country).HasColumnName("City_Country");
                dd.Property(d => d.Name).HasColumnName("City_Name");
                dd.Property(d => d.Population).HasColumnName("City_Population");
                dd.Property(d => d.Sunrise).HasColumnName("City_Sunrise");
                dd.Property(d => d.Sunset).HasColumnName("City_Sunset");
                dd.Property(d => d.Timezone).HasColumnName("City_Timezone");
                dd.Property(d => d.Lat).HasColumnName("City_Lat");
                dd.Property(d => d.Lon).HasColumnName("City_Lon");
            });

            builder.HasMany(o => o.DLists)
                .WithOne(o => o.DailyWeather)
                .HasForeignKey(o => o.DailyWeatherId);
        }
    }
}
