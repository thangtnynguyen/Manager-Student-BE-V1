using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Configurations
{
    public class QuizCourseConfiguration : IEntityTypeConfiguration<QuizCourse>
    {
        public void Configure(EntityTypeBuilder<QuizCourse> builder)
        {
            builder.ToTable("QuizCourses");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Answer).HasMaxLength(250);
        }
    }
}
