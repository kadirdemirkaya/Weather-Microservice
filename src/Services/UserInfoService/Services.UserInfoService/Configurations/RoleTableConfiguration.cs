using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.UserInfoService.Aggregates.Entities;
using Services.UserInfoService.Aggregates.ValueObjects;
using static Services.UserInfoService.Constants.Constant;

namespace Services.UserInfoService.Configurations
{
    public class RoleTableConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(Tables.Roles);

            builder.HasKey(r => r.Id);

            builder.Property(m => m.Id)
               .ValueGeneratedNever()
               .HasConversion(
                   id => id.Id,
                   value => RoleId.Create(value));

            builder.HasMany(o => o.RoleUsers)
                   .WithOne(o => o.Role);

            builder.Property(r => r.RoleEnum);

            builder.Property(r => r.IsActive);
        }
    }
}
