using Microsoft.AspNetCore.Identity;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Seeders
{
    public static class UserSeeder
    {
        public static List<User> Data()
        {
            var hasher = new PasswordHasher<User>();
            var users = new List<User>()
            {
                new User
                {
                    Id = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE"),
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    Email = "luyenhaidangit@gmail.com",
                    NormalizedEmail = "luyenhaidangit@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null!, "haidang106"),
                    SecurityStamp = string.Empty,
                    Name = "Luyện Hải Đăng",
                    AvatarUrl = "/User/AvatarDefault.png",
                    Status = true
                },
                new User
                {
                    Id = new Guid("C4F97A72-6B4A-47D3-BA1B-6FE15E62C192"),
                    UserName = "caovandan",
                    NormalizedUserName = "caovandan",
                    Email = "caovandan@gmail.com",
                    NormalizedEmail = "caovandan@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null!, "haidang106"),
                    SecurityStamp = string.Empty,
                    Name = "Cao Văn Dân",
                    AvatarUrl = "/User/AvatarDefault.png",
                    Status = true
                },
                new User
                {
                    Id = new Guid("1A3E854A-843D-4E65-AB88-9D5736C831F2"),
                    UserName = "nguyenvanthang",
                    NormalizedUserName = "nguyenvanthang",
                    Email = "nguyenvanthang@gmail.com",
                    NormalizedEmail = "nguyenvanthang@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null!, "haidang106"),
                    SecurityStamp = string.Empty,
                    Name = "Nguyễn Văn Thắng",
                    AvatarUrl = "/User/AvatarDefault.png",
                    Status = true
                },
                new User
                {
                    Id = new Guid("D5E5B63A-53A1-4F88-A399-1F7C7F4B08A6"),
                    UserName = "phamxuantuyen",
                    NormalizedUserName = "phamxuantuyen",
                    Email = "phamxuantuyen@gmail.com",
                    NormalizedEmail = "phamxuantuyen@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null!, "haidang106"),
                    SecurityStamp = string.Empty,
                    Name = "Phạm Xuân Tuyển",
                    AvatarUrl = "/User/AvatarDefault.png",
                    Status = true
                },
                new User
                {
                    Id = new Guid("D5E5B63A-53A1-4F88-A399-1F7C7F4B08A1"),
                    UserName = "daoxuanduc",
                    NormalizedUserName = "daoxuanduc",
                    Email = "daoxuanduc@gmail.com",
                    NormalizedEmail = "daoxuanduc@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null!, "haidang106"),
                    SecurityStamp = string.Empty,
                    Name = "Đào Xuân Đức",
                    AvatarUrl = "/User/AvatarDefault.png",
                    Status = true
                },
                new User
                {
                    Id = new Guid("D5E5B63A-53A1-4F88-A399-1F7C7F4B08B1"),
                    UserName = "hoanggiabao",
                    NormalizedUserName = "hoanggiabao",
                    Email = "hoanggiabao@gmail.com",
                    NormalizedEmail = "hoanggiabao@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null!, "haidang106"),
                    SecurityStamp = string.Empty,
                    Name = "Hoàng Gia Bảo",
                    AvatarUrl = "/User/AvatarDefault.png",
                    Status = true,
                    PhoneNumber = "+84922002360"
                },
                 new User
                {
                    Id = new Guid("D5E5B63A-53A1-4F88-A399-1F7C7F4B08A7"),
                    UserName = "buixuanhoang",
                    NormalizedUserName = "buixuanhoang",
                    Email = "buixuanhoang@gmail.com",
                    NormalizedEmail = "buixuanhoang@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null!, "haidang106"),
                    SecurityStamp = string.Empty,
                    Name = "Bùi Xuân Hoàng",
                    AvatarUrl = "/User/AvatarDefault.png",
                    Status = true,
                    PhoneNumber = "+84922002111"
                },
                  new User
                {
                    Id = new Guid("D5E5B63A-53A1-4F88-A399-1F7C7F4B08A2"),
                    UserName = "phamthanhlong",
                    NormalizedUserName = "phamthanhlong",
                    Email = "phamthanhlong@gmail.com",
                    NormalizedEmail = "phamthanhlong@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null!, "haidang106"),
                    SecurityStamp = string.Empty,
                    Name = "Phạm Thanh Long",
                    AvatarUrl = "/User/AvatarDefault.png",
                    Status = true,
                    PhoneNumber = "+84922002222"
                },
                   new User
                {
                    Id = new Guid("D5E5B63A-53A1-4F88-A399-1F7C7F4B08B2"),
                    UserName = "nguyendinhhung",
                    NormalizedUserName = "nguyendinhhung",
                    Email = "nguyendinhhung@gmail.com",
                    NormalizedEmail = "nguyendinhhung@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null!, "haidang106"),
                    SecurityStamp = string.Empty,
                    Name = "Nguyễn Đình Hùng",
                    AvatarUrl = "/User/AvatarDefault.png",
                    Status = true,
                    PhoneNumber = "+84922002333"
                },
            };

            return users;
        }
    }
}
