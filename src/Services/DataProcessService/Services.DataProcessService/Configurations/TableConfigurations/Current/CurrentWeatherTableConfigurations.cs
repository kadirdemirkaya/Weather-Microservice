using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.DataProcessService.Aggregate;
using Services.DataProcessService.Aggregate.ValueObjects;
using static Services.DataProcessService.Constants.Constant;

namespace Services.DataProcessService.Configurations.TableConfigurations.Current
{
    public class CurrentWeatherTableConfigurations : IEntityTypeConfiguration<CurrentWeather>
    {
        public void Configure(EntityTypeBuilder<CurrentWeather> builder)
        {
            builder.ToTable(Tables.CurrentWeathers);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
               .ValueGeneratedNever()
               .HasConversion(
                   id => id.Id,
                   value => CurrentWeatherId.Create(value));

            builder.Property(c => c.@Base);

            builder.Property(c => c.Dt);

            builder.Property(c => c.Timezone);

            builder.Property(c => c.id);

            builder.Property(c => c.Name);

            builder.Property(c => c.Cod);


            builder.OwnsOne(c => c.Coord, cc =>
            {
                cc.Property(c => c.Lat).HasColumnName("Coord_Lat");
                cc.Property(c => c.Lon).HasColumnName("Coord_Lon");
            });

            builder.OwnsOne(c => c.Main, mm =>
            {
                mm.Property(m => m.Feels_like).HasColumnName("Main_Feels_like");
                mm.Property(m => m.Grnd_level).HasColumnName("Main_Grnd_level");
                mm.Property(m => m.Humidity).HasColumnName("Main_Humidity");
                mm.Property(m => m.Pressure).HasColumnName("Main_Pressure");
                mm.Property(m => m.Sea_level).HasColumnName("Main_Sea_level");
                mm.Property(m => m.Temp).HasColumnName("Main_Temp");
                mm.Property(m => m.Temp_max).HasColumnName("Main_Temp_max");
                mm.Property(m => m.Temp_min).HasColumnName("Main_Temp_min");
            });

            builder.OwnsOne(c => c.Wind, ww =>
            {
                ww.Property(w => w.Deg).HasColumnName("Wind_Deg");
                ww.Property(w => w.Gust).HasColumnName("Wind_Gust");
                ww.Property(w => w.Speed).HasColumnName("Wind_Speed");
            });

            builder.OwnsOne(c => c.Rain, rr =>
            {
                rr.Property(r => r._1h).HasColumnName("Rain_1h").HasDefaultValue(default(double));
            });

            builder.OwnsOne(c => c.Cloud, cc =>
            {
                cc.Property(c => c.Aal).HasColumnName("Cloud_Aal");
            });

            builder.OwnsOne(c => c.Sys, ss =>
            {
                ss.Property(s => s.Country).HasColumnName("Sys_Country");
                ss.Property(s => s.id).HasColumnName("Sys_id");
                ss.Property(s => s.Sunrise).HasColumnName("Sys_Sunrise");
                ss.Property(s => s.Type).HasColumnName("Sys_Type");
            });

            builder.HasMany(o => o.CWeathers)
                .WithOne(o => o.CurrentWeather);
        }
    }
}
