using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blogs");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.MetaTitle).HasMaxLength(50);
            builder.Property(p => p.MetaDescription).HasMaxLength(200);
            builder.Property(p => p.ImageUrl).HasMaxLength(200);
        }
    }
}
