using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.UserInfoService.Aggregates.Entities;
using Services.UserInfoService.Aggregates.ValueObjects;
using static Services.UserInfoService.Constants.Constant;

namespace Services.UserInfoService.Configurations
{
    internal class RoleUserTableConfiguration : IEntityTypeConfiguration<RoleUser>
    {
        public void Configure(EntityTypeBuilder<RoleUser> builder)
        {
            builder.ToTable(Tables.RoleUsers);

            builder.HasKey(r => r.Id);

            builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Id,
                value => RoleUserId.Create(value));

            builder.Property(m => m.RoleId)
           .ValueGeneratedNever()
           .HasConversion(
               id => id.Id,
               value => RoleId.Create(value));

            builder.Property(m => m.UserId)
           .ValueGeneratedNever()
           .HasConversion(
               id => id.Id,
               value => UserId.Create(value));

            builder.Property(r => r.RoleId);

            builder.Property(r => r.UserId);

            builder.HasOne(ru => ru.Role)
                   .WithMany(r => r.RoleUsers)
                   .HasForeignKey(ru => ru.RoleId);

            builder.HasOne(ru => ru.User)
                   .WithMany(r => r.RoleUsers)
                   .HasForeignKey(ru => ru.UserId);

            builder.Property(r => r.UserRoleStatus);

            builder.Property(r => r.IsActive);
        }
    }
}
