using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Seeders
{
    public class PostSeeder
    {
        public static List<Post> Data()
        {
            var posts = new List<Post>()
            {
                new Post
                {
                    Id = 1,
                    UserId = new Guid("C4F97A72-6B4A-47D3-BA1B-6FE15E62C192"),
                    Title = "Authentication & Authorization trong ReactJS",
                    Description = "Authentication và Authorization là một phần quan trọng trong việc phát triển phần mềm, giúp chúng ta xác thực và phân quyền...",
                    Image = "/Post/Post-1.png",
                    Content = "",
                    Priority  = 0,
                    IsPublished = true,
                },
                new Post
                {
                    Id = 2,
                    UserId = new Guid("C4F97A72-6B4A-47D3-BA1B-6FE15E62C192"),
                    Title = "Hướng dẫn chi tiết cách sử dụng Dev Mode trong khóa Pro",
                    Description = "Chào bạn! Nếu bạn đã là học viên khóa Pro của F8, chắc hẳn bạn đã biết tới \"Dev Mode\" - giúp thực hành code song song khi xem...",
                    Image = "/Post/Post-2.png",
                    Content = "",
                    Priority  = 0,
                    IsPublished = true,
                },
                new Post
                {
                    Id = 3,
                    UserId = new Guid("C4F97A72-6B4A-47D3-BA1B-6FE15E62C192"),
                    Title = "Cách chỉnh theme Oh-my-posh cho Powershell",
                    Description = "Hello ae mọi người nhé, mọi người (đặc biệt là lập trình viên Software) chắc hẳn đã nghe tới Powershell, nhưng bù lại cái màn hình...",
                    Image = "/Post/Post-3.png",
                    Content = "",
                    Priority  = 0,
                    IsPublished = true,
                },
                new Post
                {
                    Id = 4,
                    UserId = new Guid("C4F97A72-6B4A-47D3-BA1B-6FE15E62C192"),
                    Title = "Sự khác biệt giữa var, let và const trong JavaScript",
                    Description = "Ở bài viết này, chúng ta cùng nhau tìm hiểu về các vấn đề liên quan đến Performance ở phía FE. Không dài dòng nữa,...",
                    Image = "/Post/Post-4.png",
                    Content = "",
                    Priority  = 0,
                    IsPublished = true,
                },
            };

            return posts;
        }
    }
}
