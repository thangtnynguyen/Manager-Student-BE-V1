using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Track
{
    public class CreateStepTrackRequest
    {
        public int TypeStepTrackId { get; set; }
        public int Position { get; set; }
        public int TrackID { get; set; }
        public int? AssignmentCourseId { get; set; }
        public int? VideoCourseId { get; set; }
        public CreateAssignmentCourseRequest? AssignmentCourse { get; set; }
        public VideoCourse? VideoCourse { get; set; }
    }
}
