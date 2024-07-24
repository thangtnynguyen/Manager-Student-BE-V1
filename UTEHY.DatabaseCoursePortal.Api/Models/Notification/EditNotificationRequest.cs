namespace UTEHY.DatabaseCoursePortal.Api.Models.Notification
{
    public class EditNotificationRequest
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public string? Message { get; set; }
    }
}
