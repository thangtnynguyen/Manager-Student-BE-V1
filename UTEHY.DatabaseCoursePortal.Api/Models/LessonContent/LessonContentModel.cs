namespace UTEHY.DatabaseCoursePortal.Api.Models.LessonContent
{
    public class LessonContentModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public bool? IsPublished { get; set; }

        public int? Type { get; set; }

        public string? VideoUrl { get; set; }

        public int? VideoType { get; set; }

        public string? FileUrl { get; set; }

        public int? LessonId { get; set; }

        public int? EstimatedCompletionTime { get; set; }

        public int? PrerequisiteLessonId { get; set; }

        public int? Priority { get; set; }

        public IFormFile? File { get; set; }
    }
}
