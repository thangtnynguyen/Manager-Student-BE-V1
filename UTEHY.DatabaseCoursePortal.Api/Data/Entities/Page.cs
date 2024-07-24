namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class Page
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        public string? Image { get; set; }

        public string? Description { get; set; }

        public string? Content { get; set; }

        public string? Url { get; set; }

        public int? Priority { get; set; }

        public string? Type { get; set; }

        public bool? Status { get; set; }
    }
}
