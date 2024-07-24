using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Configurations
{
    public class BlogTopicConfiguration : IEntityTypeConfiguration<BlogTopic>
    {
        public void Configure(EntityTypeBuilder<BlogTopic> builder)
        {
            builder.ToTable("BlogTopics");

            builder.HasKey(x => x.Id);
            builder.Property(b => b.Title).HasMaxLength(250);
            builder.Property(b => b.Slug).HasMaxLength(250);
            builder.Property(b => b.SubTitle).HasMaxLength(50);
        }
    }
}
