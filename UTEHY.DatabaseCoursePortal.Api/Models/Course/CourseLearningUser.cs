using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Course
{
    public class CourseLearningUser
    {
        public int? Id { get; set; }

        public string? CertificateName { get; set; }

        public string? Content { get; set; }

        public string? VideoUrl { get; set; }

        public int? TypeVideo { get; set; }

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public string? Title { get; set; }

        public string? Slug { get; set; }

        public string? LandingPage { get; set; }

        public int? Priority { get; set; }

        public DateTime? PublishedAt { get; set; }

        public bool? IsPublished { get; set; }

        public bool IsDefault { get; set; }

        public bool? IsRegister { get; set; }

        public List<UserCourse>? UserCourses { get; set; }

        public virtual List<Data.Entities.Lesson> Lessons { get; set; }
    }
}
