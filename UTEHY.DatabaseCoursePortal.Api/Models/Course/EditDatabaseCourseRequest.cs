namespace UTEHY.DatabaseCoursePortal.Api.Models.Course
{
    public class EditDatabaseCourseRequest
    {
        public string? Content { get; set; }

        public string? VideoUrl { get; set; }

        public int? TypeVideo { get; set; }

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public string? Title { get; set; }
    }
}
