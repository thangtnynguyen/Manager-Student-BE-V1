namespace UTEHY.DatabaseCoursePortal.Api.Models.Account
{
    public class VerifyOtpLoginPhoneRequest
    {
        public string? Name { get; set; }

        public string Phone { get; set; }

        public string Otp { get; set; }
    }
}
