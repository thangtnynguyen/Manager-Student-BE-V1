namespace UTEHY.DatabaseCoursePortal.Api.Models.Section
{
    public class EditSectionRequest
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        public int? Priority { get; set; }

        public string? Description { get; set; }

        public bool? Status { get; set; }
    }
}
