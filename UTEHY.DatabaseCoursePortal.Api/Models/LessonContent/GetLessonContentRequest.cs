using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.LessonContent
{
    public class GetLessonContentRequest : PagingRequest
    {
        public int? LessonId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
