using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Banner
{
    public class GetBannerRequest : PagingRequest
    {
        public string? Place { get; set; }

        public string? Type { get; set; }

        public string? Title { get; set; }
    }
}
