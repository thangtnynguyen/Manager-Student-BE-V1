using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Post
{
    public class GetPostRequest: PagingRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }

    }
}
