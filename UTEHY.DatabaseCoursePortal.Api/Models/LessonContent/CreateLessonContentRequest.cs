using System.Text.Json.Serialization;

namespace UTEHY.DatabaseCoursePortal.Api.Models.LessonContent
{
    public class CreateLessonContentRequest
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public bool? IsPublished { get; set; }

        public int? Type { get; set; }

        public string? VideoUrl { get; set; }

        public int? VideoType { get; set; }

        public string? FileUrl { get; set; }

        public int? LessonId { get; set; }

        public int? EstimatedStudyTime { get; set; }

        public int? PrerequisiteLessonId { get; set; }
    }
}
