namespace UTEHY.DatabaseCoursePortal.Api.Models.Mail
{
    public class SendMailRequest
    {
        public string ToEmail { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
