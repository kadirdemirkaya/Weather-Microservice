using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.DataProcessService.Aggregate.Daily.Entities;
using Services.DataProcessService.Aggregate.Daily.ValueObjects;
using static Services.DataProcessService.Constants.Constant;

namespace Services.DataProcessService.Configurations.TableConfigurations.Daily
{
    public class ListTableConfigurations : IEntityTypeConfiguration<Aggregate.Daily.Entities.DList>
    {
        public void Configure(EntityTypeBuilder<DList> builder)
        {
            builder.ToTable(Tables.DLists);

            builder.HasKey(l => l.Id);

            builder.Property(c => c.Id)
              .ValueGeneratedNever()
              .HasConversion(
                  id => id.Id,
                  value => DListId.Create(value));

            builder.Property(l => l.Dt);

            builder.Property(l => l.Visibility);

            builder.Property(l => l.Pop);

            builder.Property(l => l.Dt_txt);

            builder.OwnsOne(d => d.Main, dd =>
            {
                dd.Property(d => d.Feels_like).HasColumnName("Main_Feels_like");
                dd.Property(d => d.Grnd_level).HasColumnName("Main_Grnd_level");
                dd.Property(d => d.Humidity).HasColumnName("Main_Humidity");
                dd.Property(d => d.Pressure).HasColumnName("Main_Pressure");
                dd.Property(d => d.Sea_level).HasColumnName("Main_Sea_level");
                dd.Property(d => d.Temp).HasColumnName("Main_Temp");
                dd.Property(d => d.Temp_kf).HasColumnName("Main_Temp_kf");
                dd.Property(d => d.Temp_max).HasColumnName("Main_Temp_max");
                dd.Property(d => d.Temp_min).HasColumnName("Main_Temp_min");
            });

            builder.OwnsOne(d => d.Clouds, dd =>
            {
                dd.Property(d => d.All).HasColumnName("Clouds_All");
            });

            builder.OwnsOne(d => d.Wind, dd =>
            {
                dd.Property(d => d.Deg).HasColumnName("Wind_Deg");
                dd.Property(d => d.Gust).HasColumnName("Wind_Gust");
                dd.Property(d => d.Speed).HasColumnName("Wind_Speed");
            });

            builder.OwnsOne(d => d.Wind, dd =>
            {
                dd.Property(d => d.Deg).HasColumnName("Wind_Deg");
                dd.Property(d => d.Gust).HasColumnName("Wind_Gust");
                dd.Property(d => d.Speed).HasColumnName("Wind_Speed");
            });

            builder.OwnsOne(d => d.Rain, dd =>
            {
                dd.Property(d => d._3h).HasColumnName("Rain__3h").HasDefaultValue(default(double));
            });

            builder.OwnsOne(d => d.Sys, dd =>
            {
                dd.Property(d => d.Pod).HasColumnName("Sys_Pod");
            });

            builder.HasOne(l => l.DailyWeather)
                    .WithMany(d => d.DLists)
                    .HasForeignKey(d => d.DailyWeatherId);

            builder.HasMany(l => l.Dweather)
                .WithOne(w => w.DList)
                .HasForeignKey(w => w.DListId);
        }
    }
}
