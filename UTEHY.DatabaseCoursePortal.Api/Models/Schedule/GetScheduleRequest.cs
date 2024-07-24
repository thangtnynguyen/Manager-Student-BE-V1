using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Schedule
{
    public class GetScheduleRequest : PagingRequest
    {
        public int? GroupModuleId { get; set; }
        public string? GroupModuleName { get; set; }
    }
}
