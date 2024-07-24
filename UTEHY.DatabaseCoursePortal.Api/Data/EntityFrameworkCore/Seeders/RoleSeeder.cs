using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Seeders
{
    public class RoleSeeder
    {
        public static List<Role> Data()
        {
            var roles = new List<Role>()
            {
                new Role
                {
                    Id = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC"),
                    Name = "admin",
                    NormalizedName = "admin",
                    Description = "Admin Role"
                },
                new Role
                {
                    Id = new Guid("C3F087A2-48D5-4E09-8A63-8830A7B5B4E3"),
                    Name = "student",
                    NormalizedName = "student",
                    Description = "Student Role"
                },
                new Role
                {
                    Id = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DD"),
                    Name = "teacher",
                    NormalizedName = "teacher",
                    Description = "Teacher Role"
                },
            };

            return roles;
        }
    }
}
