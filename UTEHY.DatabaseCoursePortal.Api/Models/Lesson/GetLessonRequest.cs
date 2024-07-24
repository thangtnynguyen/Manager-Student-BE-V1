using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Lesson
{
    public class GetLessonRequest : PagingRequest
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
