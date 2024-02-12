using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.DataProcessService.Aggregate.Air.Entities;
using Services.DataProcessService.Aggregate.Air.ValueObjects;
using static Services.DataProcessService.Constants.Constant;

namespace Services.DataProcessService.Configurations.TableConfigurations.Air
{
    public class ListTableConfigurations : IEntityTypeConfiguration<Aggregate.Air.Entities.AList>
    {
        public void Configure(EntityTypeBuilder<AList> builder)
        {
            builder.ToTable(Tables.ALists);

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
               .ValueGeneratedNever()
               .HasConversion(
                   id => id.Id,
                   value => AListId.Create(value));

            builder.Property(l => l.Dt);

            builder.OwnsOne(c => c.Main, cc =>
            {
                cc.Property(c => c.Aqi).HasColumnName("Main_Aqi");
            });

            builder.OwnsOne(c => c.Components, cc =>
            {
                cc.Property(c => c.Co).HasColumnName("Component_Co");
                cc.Property(c => c.Nh3).HasColumnName("Component_Nh3");
                cc.Property(c => c.No).HasColumnName("Component_No");
                cc.Property(c => c.No2).HasColumnName("Component_No2");
                cc.Property(c => c.O3).HasColumnName("Component_O3");
                cc.Property(c => c.Pm10).HasColumnName("Component_Pm10");
                cc.Property(c => c.Pm2).HasColumnName("Component_Pm2");
                cc.Property(c => c.So2).HasColumnName("Component_So2");
            });

            builder.HasOne(l => l.AirPollutionWeather)
                   .WithMany(l => l.ALists)
                   .HasForeignKey(l => l.AirPollutionWeatherId);
        }
    }
}
