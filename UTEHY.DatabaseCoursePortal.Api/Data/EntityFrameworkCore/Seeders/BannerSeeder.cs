using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Seeders
{
    public class BannerSeeder
    {
        public static List<Banner> Data()
        {
            var banners = new List<Banner>()
            {
                new Banner
                {
                    Id = 1,
                    Place = "Home",
                    Type = "SlideShow",
                    Image = "/Banner/Banner-1.png",
                    Title = "Học HTML CSS cho người mới",
                    Description = "Thực hành dự án với Figma, hàng trăm bài tập và thử thách, hướng dẫn 100% bởi Sơn Đặng, tặng kèm Flashcards, v.v.",
                    Alt = "Banner-1",
                    CtaTitle = "Học thử miễn phí",
                    LinkTo = "https://fullstack.edu.vn/landing/htmlcss/",
                    Properties = "{\"props\":{\"style\":{\"--cta-hover-color\":\"#6828fa\",\"background\":\"linear-gradient(to right, #6828fa, #ffbaa4)\"}}}",
                    IsBlank = true,
                    Priority = 1,
                    Expired = new DateTime(2023, 12, 31)
                },
                new Banner
                {
                    Id = 2,
                    Place = "Home",
                    Type = "SlideShow",
                    Image = "/Banner/Banner-2.png",
                    Title = "Học ReactJS Miễn Phí!",
                    Description = "Khóa học ReactJS từ cơ bản tới nâng cao. Kết quả của khóa học này là bạn có thể làm hầu hết các dự án thường gặp với ReactJS.",
                    Alt = "Banner-2",
                    CtaTitle = "Đăng ký ngay",
                    LinkTo = "https://fullstack.edu.vn/courses/reactjs?ref=banner",
                    Properties = "{\"props\":{\"style\":{\"--cta-hover-color\":\"#2877FA\",\"background\":\"linear-gradient(to right, #2877FA, #6717CD)\"}}}",
                    IsBlank = true,
                    Priority = 2,
                    Expired = new DateTime(2023, 12, 31)
                },
                new Banner
                {
                    Id = 3,
                    Place = "Home",
                    Type = "SlideShow",
                    Image = "/Banner/Banner-3.png",
                    Title = "Thành Quả của Học Viên",
                    Description = "Để đạt được kết quả tốt trong mọi việc ta cần xác định mục tiêu rõ ràng cho việc đó. Học lập trình cũng không là ngoại lệ.",
                    Alt = "Banner-3",
                    CtaTitle = "Xem thành quả",
                    LinkTo = "https://fullstack.edu.vn/blog/tong-hop-cac-san-pham-cua-hoc-vien-tai-f8.html",
                    Properties = "{\"props\":{\"style\":{\"--cta-hover-color\":\"#7612ff\",\"background\":\"linear-gradient(to right, #7612ff, #05b2ff)\"}}}",
                    IsBlank = true,
                    Priority = 3,
                    Expired = new DateTime(2023, 12, 31)
                },
            };

            return banners;
        }
    }
}
