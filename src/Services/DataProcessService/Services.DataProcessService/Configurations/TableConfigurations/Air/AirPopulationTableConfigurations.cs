using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.DataProcessService.Aggregate.Air;
using Services.DataProcessService.Aggregate.Air.ValueObjects;
using static Services.DataProcessService.Constants.Constant;

namespace Services.DataProcessService.Configurations.TableConfigurations.Air
{
    public class AirPopulationTableConfigurations : IEntityTypeConfiguration<AirPollutionWeather>
    {
        public void Configure(EntityTypeBuilder<AirPollutionWeather> builder)
        {
            builder.ToTable(Tables.AirPollutionWeathers);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedNever()
               .HasConversion(
                   id => id.Id,
                   value => AirPollutionWeatherId.Create(value));

            builder.OwnsOne(c => c.Coord, cc =>
            {
                cc.Property(c => c.Latitude).HasColumnName("Coord_Latitude");
                cc.Property(c => c.Longitude).HasColumnName("Coord_Longitude");
            });

            builder.HasMany(a => a.ALists)
                   .WithOne(a => a.AirPollutionWeather)
                   .HasForeignKey(a => a.AirPollutionWeatherId);
        }
    }
}
