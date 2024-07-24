using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Configurations
{
    public class StepTrackConfiguration : IEntityTypeConfiguration<StepTrack>
    {
        public void Configure(EntityTypeBuilder<StepTrack> builder)
        {
            builder.ToTable("StepTracks");

            builder.HasKey(t => t.Id);
        }
    }
}
