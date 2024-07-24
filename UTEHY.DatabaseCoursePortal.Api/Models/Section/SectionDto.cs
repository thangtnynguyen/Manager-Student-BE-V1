using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Section
{
    public class SectionDto : EntityBaseInfo
    {
        public int? Id { get; set; }

        public int? CourseId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public int? Priority { get; set; }

        public bool? Status { get; set; }
    }
}
