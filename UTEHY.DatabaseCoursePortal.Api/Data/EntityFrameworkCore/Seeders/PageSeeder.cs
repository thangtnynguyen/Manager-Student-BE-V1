using System;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Seeders
{
    public class PageSeeder
    {
        public static List<Page> Data()
        {
            var pages = new List<Page>()
            {
                new Page
                {
                    Id = 1,
                    Title = "Vẽ sơ đồ ER",
                    Image  = "/Page/Page-1.png",
                    Description = "Công cụ sơ đồ ER để trực quan hóa cách các thực thể hệ thống như người hoặc đối tượng liên quan với nhau.",
                    Content = null,
                    Url = null,
                    Priority = 4,
                    Type = "Feature",
                    Status = true
                },
                new Page
                {
                    Id = 2,
                    Title = "Bộ câu hỏi trắc nghiệm về CSDL",
                    Image  = "/Page/Page-2.png",
                    Description = "Tổng hợp câu hỏi trắc nghiệm CSDL có đáp án đầy đủ nhằm giúp các bạn dễ dàng ôn tập lại toàn bộ các kiến thức.",
                    Content = null,
                    Url = null,
                    Priority = 3,
                    Type = "Feature",
                    Status = true
                },
                new Page
                {
                    Id = 3,
                    Title = "Thực thi SQL",
                    Image  = "/Page/Page-3.png",
                    Description = "Trình biên dịch SQL trực tuyến ngay trên nền tảng web, không cần phải cài đặt trên máy tính cá nhân.",
                    Content = null,
                    Url = null,
                    Priority = 2,
                    Type = "Feature",
                    Status = true
                },
                new Page
                {
                    Id = 4,
                    Title = "Thảo luận và diễn dàn",
                    Image  = "/Page/Page-4.png",
                    Description = "Nơi người dùng có thể tương tác, chia sẻ kiến thức, và giải quyết thắc mắc về CSDL.",
                    Content = null,
                    Url = null,
                    Priority = 1,
                    Type = "Feature",
                    Status = true
                },
            };

            return pages;
        }
    }
}
