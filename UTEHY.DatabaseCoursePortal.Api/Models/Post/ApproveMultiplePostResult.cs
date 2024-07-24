using UTEHY.DatabaseCoursePortal.Api.Models.Comment;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.User;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Post
{
    public class ApproveMultipleCommentResult<T>
    {
        public List<T>? SuccessfulItems { set; get; }

        public List<T>? FailedItems { set; get; }


    }
}
