using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Configurations
{
    public class BannerConfiguration : IEntityTypeConfiguration<Banner>
    {
        public void Configure(EntityTypeBuilder<Banner> builder)
        {
            builder.ToTable("Banners");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Image)
            .IsRequired()
            .HasMaxLength(4000);

            builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(255);

            builder.Property(b => b.Description)
            .HasMaxLength(4000);

            builder.Property(b => b.Alt)
            .HasMaxLength(255);

            builder.Property(b => b.CtaTitle)
            .HasMaxLength(255);

            builder.Property(b => b.LinkTo)
            .HasMaxLength(4000);

            builder.Property(b => b.Properties)
            .HasMaxLength(4000);

            builder.Property(b => b.IsBlank)
            .IsRequired();

            builder.Property(b => b.Priority)
            .IsRequired();

            builder.Property(b => b.Expired);
        }
    }
}
