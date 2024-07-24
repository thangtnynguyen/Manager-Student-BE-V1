using UTEHY.DatabaseCoursePortal.Api.Models.Comment;
using UTEHY.DatabaseCoursePortal.Api.Models.User;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Post
{
    public class ApprovePostRequest
    {
        public int? Id { get; set; }
        public bool? IsApproved { get; set;}

    }
}
