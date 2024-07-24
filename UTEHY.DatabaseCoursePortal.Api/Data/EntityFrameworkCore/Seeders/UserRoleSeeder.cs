using Microsoft.AspNetCore.Identity;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Seeders
{
    public class UserRoleSeeder
    {
        public static List<UserRole> Data()
        {
            var userRoles = new List<UserRole>()
            {
                new UserRole
                {
                     UserId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE"),
                     RoleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC")
                },
                new UserRole
                {
                     UserId = new Guid("C4F97A72-6B4A-47D3-BA1B-6FE15E62C192"),
                     RoleId = new Guid("C3F087A2-48D5-4E09-8A63-8830A7B5B4E3")
                },
                new UserRole
                {
                     UserId = new Guid("1A3E854A-843D-4E65-AB88-9D5736C831F2"),
                     RoleId = new Guid("C3F087A2-48D5-4E09-8A63-8830A7B5B4E3")
                },
                //User: Tuyển, Role: Teacher
                new UserRole
                {
                     UserId = new Guid("D5E5B63A-53A1-4F88-A399-1F7C7F4B08A6"),
                     RoleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DD")
                },
                 //User: Đức, Role: Teacher
                new UserRole
                {
                     UserId = new Guid("D5E5B63A-53A1-4F88-A399-1F7C7F4B08A1"),
                     RoleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DD")
                },
                //User: Bảo, Role: Teacher
                new UserRole
                {
                     UserId = new Guid("D5E5B63A-53A1-4F88-A399-1F7C7F4B08B1"),
                     RoleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DD")
                },
            };

            return userRoles;
        }
    }
}
