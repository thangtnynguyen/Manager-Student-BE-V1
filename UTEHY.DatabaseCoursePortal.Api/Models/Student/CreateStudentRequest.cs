using UTEHY.DatabaseCoursePortal.Api.Enums;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Student
{
    public class CreateStudentRequest
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public bool? Status { get; set; }

        public string? StudentId { get; set; }

        /// <summary>
        /// 1.Điện thoại 2.Email
        /// </summary>
        public VerificationType VerificationType { get; set; }
    }
}
