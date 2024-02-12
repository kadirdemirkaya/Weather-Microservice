using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.DataProcessService.Aggregate.Daily.Entities;
using Services.DataProcessService.Aggregate.Daily.ValueObjects;
using static Services.DataProcessService.Constants.Constant;

namespace Services.DataProcessService.Configurations.TableConfigurations.Daily
{
    internal class DWeatherTableConfigurations : IEntityTypeConfiguration<Aggregate.Daily.Entities.DWeather>
    {
        public void Configure(EntityTypeBuilder<DWeather> builder)
        {
            builder.ToTable(Tables.DWeathers);

            builder.HasKey(w => w.Id);

            builder.Property(c => c.Id)
              .ValueGeneratedNever()
              .HasConversion(
                  id => id.Id,
                  value => WeatherId.Create(value));

            builder.Property(c => c.DListId)
              .ValueGeneratedNever()
              .HasConversion(
                  id => id.Id,
                  value => DListId.Create(value));

            builder.Property(w => w.id);

            builder.Property(w => w.Main);

            builder.Property(w => w.Description);

            builder.Property(w => w.Icon);

            builder.HasOne(w => w.DList)
                   .WithMany(l => l.Dweather)
                   .HasForeignKey(w => w.DListId);
        }
    }
}
