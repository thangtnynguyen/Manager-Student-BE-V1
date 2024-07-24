using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.User;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Teacher
{
    public class TeacherDto
    {
        public int? Id { get; set; }

        public Guid? UserId { get; set; }

        public string? AvatarUrl { get; set; }

        public bool? Status { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? TeacherId { get; set; }
    }
}
