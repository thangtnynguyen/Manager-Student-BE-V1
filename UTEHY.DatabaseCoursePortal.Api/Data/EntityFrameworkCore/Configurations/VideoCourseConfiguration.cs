using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Configurations
{
    public class VideoCourseConfiguration : IEntityTypeConfiguration<VideoCourse>
    {
        public void Configure(EntityTypeBuilder<VideoCourse> builder)
        {
            builder.ToTable("VideoCourses");

            builder.HasKey(x => x.Id);
        }
    }
}
