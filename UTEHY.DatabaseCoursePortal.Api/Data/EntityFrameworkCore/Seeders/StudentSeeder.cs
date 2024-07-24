using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Seeders
{
    public class StudentSeeder
    {
        public static List<Student> Data()
        {
            var students = new List<Student>()
            {
                new Student
                {
                    Id = 1,
                    UserId = new Guid("D5E5B63A-53A1-4F88-A399-1F7C7F4B08A7"),
                    StudentId = "10121910"
                },
                new Student
                {
                    Id = 2,
                    UserId = new Guid("D5E5B63A-53A1-4F88-A399-1F7C7F4B08A2"),
                    StudentId = "10121911"
                },
                new Student
                {
                    Id = 3,
                    UserId = new Guid("D5E5B63A-53A1-4F88-A399-1F7C7F4B08B2"),
                    StudentId = "10121912"
                },
            };

            return students;
        }
    }
}
