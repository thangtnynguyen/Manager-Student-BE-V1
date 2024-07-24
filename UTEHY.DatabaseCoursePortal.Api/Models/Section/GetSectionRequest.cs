using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Section
{
    public class GetSectionRequest : PagingRequest
    {
        public string? Title { get; set; }
    }
}
