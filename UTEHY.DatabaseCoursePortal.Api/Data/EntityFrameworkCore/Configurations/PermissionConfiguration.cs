using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using Twilio.TwiML.Voice;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired();

            builder.Property(p => p.DisplayName).IsRequired();

             builder.HasOne(p => p.ParentPermission)
            .WithMany()
            .HasForeignKey(p => p.ParentPermissionId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false); 

            builder.HasQueryFilter(x => x.DeletedAt == null);
        }
    }
}
