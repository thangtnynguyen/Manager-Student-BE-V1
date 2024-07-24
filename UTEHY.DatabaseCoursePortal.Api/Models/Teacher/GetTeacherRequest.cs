using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Teacher
{
    public class GetTeacherRequest : PagingRequest
    {
        public string? NameOrEmail { get; set; }

        public string? PhoneNumber { get; set; }

        public bool? Status { get; set; }
    }
}
