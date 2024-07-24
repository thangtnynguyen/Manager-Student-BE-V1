using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Notification
{
    public class GetNotificationRequest : PagingRequest
    {
        public string? Title { get; set; }

        public Guid? CreateBy { get; set; }
    }
}
