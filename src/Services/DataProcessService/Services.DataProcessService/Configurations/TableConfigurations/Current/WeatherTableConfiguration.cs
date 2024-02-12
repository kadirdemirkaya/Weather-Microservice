using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.DataProcessService.Aggregate.Current.ValueObjects;
using Services.DataProcessService.Aggregate.ValueObjects;
using static Services.DataProcessService.Constants.Constant;

namespace Services.DataProcessService.Configurations.TableConfigurations.Current
{
    public class WeatherTableConfiguration : IEntityTypeConfiguration<Aggregate.Current.Entities.CWeather>
    {
        public void Configure(EntityTypeBuilder<Aggregate.Current.Entities.CWeather> builder)
        {
            builder.ToTable(Tables.CWeathers);

            builder.HasKey(w => w.Id);

            builder.Property(m => m.Id)
             .ValueGeneratedNever()
             .HasConversion(
                 id => id.Id,
                 value => WeatherId.Create(value));


            builder.Property(m => m.CurrentWeatherId)
             .ValueGeneratedNever()
             .HasConversion(
                 id => id.Id,
                 value => CurrentWeatherId.Create(value));

            builder.Property(w => w.id);

            builder.Property(w => w.Main);

            builder.Property(w => w.Description);

            builder.Property(w => w.Icon);

            builder.HasOne(w => w.CurrentWeather)
                   .WithMany(c => c.CWeathers)
                   .HasForeignKey(c => c.CurrentWeatherId);
        }
    }
}
