using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBlockTest.Models.Configurations.Weather
{
    public class WeatherConfiguration : IEntityTypeConfiguration<BBlockTest.Aggregate.Weather.Weather>
    {
        public void Configure(EntityTypeBuilder<BBlockTest.Aggregate.Weather.Weather> builder)
        {
            builder.ToTable("Weathers");

            builder.HasKey(p => p.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Id,
                    value => BBlockTest.Aggregate.Weather.WeatherId.Create(value));

            builder.Property(o => o.Degree);
        }
    }
}
