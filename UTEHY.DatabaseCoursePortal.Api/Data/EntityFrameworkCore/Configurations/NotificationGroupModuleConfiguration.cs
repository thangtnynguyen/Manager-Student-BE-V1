using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Configurations
{
    public class NotificationGroupModuleConfiguration : IEntityTypeConfiguration<NotificationGroupModule>
    {
        public void Configure(EntityTypeBuilder<NotificationGroupModule> builder)
        {
            builder.ToTable("NotificationGroupModules");

            builder.HasKey(b => b.Id);
        }
    }
}
