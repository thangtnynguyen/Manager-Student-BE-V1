using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Attendence
{
    public class GetAttendenceRequest : PagingRequest
    {
        public string? StudentId { get; set; }
        public int? ScheduleId { get; set; }
    }
}
