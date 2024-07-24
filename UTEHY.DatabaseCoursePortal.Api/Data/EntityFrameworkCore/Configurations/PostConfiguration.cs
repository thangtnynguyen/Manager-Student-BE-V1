using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");

            builder.HasKey(b => b.Id);

            builder.HasMany(p => p.Comments)
            .WithOne();
            builder.HasOne(p => p.User)
            .WithMany();
            builder.HasQueryFilter(x => x.DeletedAt == null);
            builder.HasQueryFilter(x => x.IsApproved == null);

        }
    }
}
