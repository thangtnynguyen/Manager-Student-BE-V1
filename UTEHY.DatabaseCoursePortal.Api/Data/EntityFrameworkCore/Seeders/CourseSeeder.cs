using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Seeders
{
    public class CourseSeeder
    {
        public static List<Course> Data()
        {
            var courses = new List<Course>()
            {
                new Course
                {
                    Id = 1,
                    Title = "Cơ sở dữ liệu",
                    ImageUrl = "/Course/Default.jpg",
                    IsDefault = true,
                },
                new Course
                {
                    Id = 2,
                    Title = "HTML CSS từ Zero đến Hero",
                    ImageUrl = "/Course/Course-2.png",
                    IsDefault = false,
                },
                new Course
                {
                    Id = 3,
                    Title = "Responsive Với Grid System",
                    ImageUrl = "/Course/Course-3.png",
                    IsDefault = true,
                },
                new Course
                {
                    Id = 4,
                    Title = "Lập Trình JavaScript Cơ Bản",
                    ImageUrl = "/Course/Course-4.png",
                    IsDefault = true,
                },
            };

            return courses;
        }
    }
}
