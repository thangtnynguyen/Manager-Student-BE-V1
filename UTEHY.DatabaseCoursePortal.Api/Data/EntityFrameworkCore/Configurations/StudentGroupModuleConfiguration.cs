using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Configurations
{
    public class StudentGroupModuleConfiguration : IEntityTypeConfiguration<StudentGroupModule>
    {
        public void Configure(EntityTypeBuilder<StudentGroupModule> builder)
        {
            builder.ToTable("StudentGroupModules");

            builder.HasKey(x => x.Id);
        }
    }
}
