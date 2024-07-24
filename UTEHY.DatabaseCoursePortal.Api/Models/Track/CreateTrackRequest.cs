namespace UTEHY.DatabaseCoursePortal.Api.Models.Track
{
    public class CreateTrackRequest
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }
    }
}
