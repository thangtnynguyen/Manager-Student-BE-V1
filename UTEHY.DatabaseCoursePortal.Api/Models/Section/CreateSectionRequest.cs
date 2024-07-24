namespace UTEHY.DatabaseCoursePortal.Api.Models.Section
{
    public class CreateSectionRequest
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public int? Priority { get; set; }

        public bool? Status { get; set; }
    }
}
