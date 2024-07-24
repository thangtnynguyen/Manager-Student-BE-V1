using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Configurations
{
    public class ExamGroupModuleConfiguration : IEntityTypeConfiguration<ExamGroupModule>
    {
        public void Configure(EntityTypeBuilder<ExamGroupModule> builder)
        {
            builder.ToTable("ExamGroupModules");

            builder.HasKey(b => b.Id);
        }
    }
}
