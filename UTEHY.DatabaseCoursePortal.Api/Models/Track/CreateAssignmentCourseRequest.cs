namespace UTEHY.DatabaseCoursePortal.Api.Models.Track
{
    public class CreateAssignmentCourseRequest
    {
        public string Title { get; set; }
        public string Question { get; set; }
        public List<QuizCourseRequest>? QuizCourses { get; set; }
    }
}
