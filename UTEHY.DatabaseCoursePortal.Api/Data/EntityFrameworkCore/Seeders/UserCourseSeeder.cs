using Microsoft.AspNetCore.Identity;
using System;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Seeders
{
    public class UserCourseSeeder
    {
        public static List<UserCourse> Data()
        {
            var userCourses = new List<UserCourse>()
            {
                new UserCourse
                {
                     Id = 1,
                     UserId = new Guid("C4F97A72-6B4A-47D3-BA1B-6FE15E62C192"),
                     CourseId = 1,
                     RegistrationDate = new DateTime(2023, 11, 2),
                     Progress = 50,
                     TimeSpent = 50,
                     Status = true,
                },
                new UserCourse
                {
                     Id = 2,
                     UserId = new Guid("1A3E854A-843D-4E65-AB88-9D5736C831F2"),
                     CourseId = 1,
                     RegistrationDate = new DateTime(2023, 11, 2),
                     Progress = 50,
                     TimeSpent = 50,
                     Status = true,
                },
                new UserCourse
                {
                     Id = 3,
                     UserId = new Guid("1A3E854A-843D-4E65-AB88-9D5736C831F2"),
                     CourseId = 2,
                     RegistrationDate = new DateTime(2023, 11, 2),
                     Progress = 50,
                     TimeSpent = 50,
                     Status = true,
                },
            };

            return userCourses;
        }
    }
}
