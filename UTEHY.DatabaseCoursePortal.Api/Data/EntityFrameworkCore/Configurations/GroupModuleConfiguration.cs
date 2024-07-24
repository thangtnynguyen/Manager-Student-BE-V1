using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Configurations
{
    public class GroupModuleConfiguration : IEntityTypeConfiguration<GroupModule>
    {
        public void Configure(EntityTypeBuilder<GroupModule> builder)
        {
            builder.ToTable("GroupModules");

            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => x.DeletedAt == null);
        }
    }
}
