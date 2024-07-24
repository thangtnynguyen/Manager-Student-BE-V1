using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class StepTrack : EntityBase
    {
        public int Id { get; set; }
        public int TypeStepTrackId { get; set; }
        public int TrackID { get; set; }
        public int Position { get; set; }
        public int? AssignmentCourseId { get; set; }
        public int? VideoCourseId { get; set; }
    }
}
