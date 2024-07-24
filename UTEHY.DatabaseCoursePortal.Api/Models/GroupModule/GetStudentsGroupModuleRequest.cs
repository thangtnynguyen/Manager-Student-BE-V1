using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.GroupModule
{
    public class GetStudentsGroupModuleRequest : PagingRequest
    {
        public int? GroupModuleId { get; set; }

        public string? NameOrEmail { get; set; }
    }
}
