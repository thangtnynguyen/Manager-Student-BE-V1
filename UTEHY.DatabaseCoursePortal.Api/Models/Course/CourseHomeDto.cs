using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Course
{
    public class CourseHomeDto
    {
        public int? Id { get; set; }

        public string? CertificateName { get; set; }

        public string? Content { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public string? Title { get; set; }

        public string? Slug { get; set; }

        public string? LandingPage { get; set; }

        public int? Priority { get; set; }

        public DateTime? PublishedAt { get; set; }

        public bool? IsPublished { get; set; }

        public int? NumberRegistrations { get; set; }

        public List<Data.Entities.Lesson> Lessons { get; set; }
    }
}
