using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.UserInfoService.Aggregates.ValueObjects;
using static Services.UserInfoService.Constants.Constant;
using Services.UserInfoService.Aggregates;

namespace Services.UserInfoService.Configurations
{
    internal class UserTableConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(Tables.Users);

            builder.HasKey(o => o.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Id,
                    value => UserId.Create(value));

            builder.HasMany(o => o.RoleUsers)
                   .WithOne(o => o.User);

            builder.Property(o => o.CreatedDate);

            builder.Property(o => o.Email);

            builder.Property(o => o.Password);

            builder.Property(o => o.UserStatus);

            builder.Property(o => o.RefreshToken);

            builder.Property(o => o.RefreshTokenEndDate);
        }
    }
}
