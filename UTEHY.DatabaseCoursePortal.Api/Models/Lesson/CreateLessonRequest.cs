namespace UTEHY.DatabaseCoursePortal.Api.Models.Lesson
{
    public class CreateLessonRequest
    {
        public string Title { get; set; }

        public string? Content { get; set; }

        public bool Status { get; set; }

        public int SectionId { get; set; }

        public int Priority { get; set; }
    }
}
