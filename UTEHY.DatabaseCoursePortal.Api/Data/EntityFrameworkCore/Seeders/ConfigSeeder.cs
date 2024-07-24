using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Seeders
{
    public class ConfigSeeder
    {
        public static List<Config> Data()
        {
            var configs = new List<Config>()
            {
                new Config
                {
                    Id = 1,
                    Key = "MaxCourseHome",
                    Value = "20",
                    Description = "Số lượng khoá học hiển thị tối đa tại trang chủ"
                },
                new Config
                {
                    Id = 2,
                    Key = "MaxBannerHome",
                    Value = "10",
                    Description = "Số lượng banner hiển thị tối đa tại trang chủ"
                },
                new Config
                {
                    Id = 3,
                    Key = "MaxPageFeatureHome",
                    Value = "10",
                    Description = "Số lượng trang tính năng hiển thị tối đa tại trang chủ"
                },
                new Config
                {
                    Id = 4,
                    Key = "MaxPostHome",
                    Value = "10",
                    Description = "Số lượng bài viết hiển thị tối đa tại trang chủ"
                },
                new Config
                {
                    Id = 5,
                    Key = "IdChannelYoutube",
                    Value = "UCVy9p7eBCMmHVEITk4yPZjg",
                    Description = "Id kênh youtube hệ thống"
                },
                new Config
                {
                    Id = 6,
                    Key = "MaxVideoYoutubeHome",
                    Value = "10",
                    Description = "Số lượng video youtube hiển thị tối đa tại trang chủ"
                },
                new Config
                {
                    Id = 7,
                    Key = "UserCreationCount",
                    Value = "0",
                    Description = "Số người dùng đã tạo"
                },
            };

            return configs;
        }
    }
}
