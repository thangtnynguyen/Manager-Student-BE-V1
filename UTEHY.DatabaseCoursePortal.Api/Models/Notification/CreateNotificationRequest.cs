namespace UTEHY.DatabaseCoursePortal.Api.Models.Notification
{
    public class CreateNotificationRequest
    {
        public string? Title { get; set; }

        public string? Message { get; set; }


        public List<int>? GroupModuleIds { get; set; }
    }
}
