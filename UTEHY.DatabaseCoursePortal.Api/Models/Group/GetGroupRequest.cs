using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Group
{
    public class GetGroupRequest : PagingRequest
    {
        public string? Name { get; set; }

        public int? Type { get; set; }
    }
}
