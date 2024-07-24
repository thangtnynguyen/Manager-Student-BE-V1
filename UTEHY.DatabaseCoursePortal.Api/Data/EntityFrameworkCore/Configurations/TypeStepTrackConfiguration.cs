using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Configurations
{
    public class TypeStepTrackConfiguration : IEntityTypeConfiguration<TypeStepTrack>
    {
        public void Configure(EntityTypeBuilder<TypeStepTrack> builder)
        {
            builder.ToTable("TypeStepTracks");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.TypeStepTrackName).HasMaxLength(250);
        }
    }
}
