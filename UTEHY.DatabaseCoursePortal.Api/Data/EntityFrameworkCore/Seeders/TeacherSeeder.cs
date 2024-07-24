using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Seeders
{
    public class TeacherSeeder
    {
        public static List<Teacher> Data()
        {
            var teachers = new List<Teacher>()
            {
                new Teacher
                {
                    Id = 1,
                    UserId = new Guid("D5E5B63A-53A1-4F88-A399-1F7C7F4B08A6"),
                    TeacherId = "12520001"
                },
                new Teacher
                {
                    Id = 2,
                    UserId = new Guid("D5E5B63A-53A1-4F88-A399-1F7C7F4B08A1"),
                    TeacherId = "12520002"
                },
                new Teacher
                {
                    Id = 3,
                    UserId = new Guid("D5E5B63A-53A1-4F88-A399-1F7C7F4B08B1"),
                    TeacherId = "12520003"
                },
            };

            return teachers;
        }
    }
}
