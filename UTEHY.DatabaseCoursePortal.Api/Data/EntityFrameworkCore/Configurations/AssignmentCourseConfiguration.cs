using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Configurations
{
    public class AssignmentCourseConfiguration : IEntityTypeConfiguration<AssignmentCourse>
    {
        public void Configure(EntityTypeBuilder<AssignmentCourse> builder)
        {
            builder.ToTable("AssignmentCourses");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(250);
        }
    }
}
