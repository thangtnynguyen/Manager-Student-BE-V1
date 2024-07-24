namespace UTEHY.DatabaseCoursePortal.Api.Models.Lesson
{
    public class EditLessonRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Content { get; set; }

        public bool Status { get; set; }

        public int SectionId { get; set; }

        public int Priority { get; set; }
    }
}
