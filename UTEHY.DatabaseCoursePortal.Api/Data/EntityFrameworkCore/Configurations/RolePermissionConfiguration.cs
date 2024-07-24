using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using Twilio.TwiML.Voice;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermissions");

            builder.HasKey(p => p.Id);

            builder.HasIndex(rp => new { rp.RoleId, rp.PermessionId }).IsUnique();

            builder.HasOne(rp => rp.Permission)
               .WithMany(r => r.RolePermissions)
               .HasForeignKey(rp => rp.PermessionId)
               .IsRequired();

            builder.HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId)
                .IsRequired();
        }
    }
}
