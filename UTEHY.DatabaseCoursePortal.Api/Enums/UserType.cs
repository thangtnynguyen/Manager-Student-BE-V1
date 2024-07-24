using System.ComponentModel;

namespace UTEHY.DatabaseCoursePortal.Api.Enums
{
    public enum UserType
    {
        [Description("Sinh viên")]
        Student = 1,

        [Description("Giáo viên")]
        Teacher = 2,

        [Description("Quản trị")]
        Admin = 3,
    }
}
