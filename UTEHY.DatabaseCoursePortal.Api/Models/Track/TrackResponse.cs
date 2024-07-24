namespace UTEHY.DatabaseCoursePortal.Api.Models.Track
{
    public class TrackResponse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }
        public List<CreateStepTrackRequest>? StepTracks { get; set; }
    }
}
