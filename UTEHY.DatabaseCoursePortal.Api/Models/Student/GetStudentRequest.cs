using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Student
{
    public class GetStudentRequest: PagingRequest
    {
        public string? NameOrEmail { get; set; }

        public string? PhoneNumber { get; set; }

        public bool? Status { get; set; }
    }
}
