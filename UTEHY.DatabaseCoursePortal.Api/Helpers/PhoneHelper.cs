namespace UTEHY.DatabaseCoursePortal.Api.Helpers
{
    public class PhoneHelper
    {
        public static string? FormatPhoneNumber(string? phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return null;
            }

            if (phoneNumber.StartsWith("+84"))
            {
                phoneNumber = "0" + phoneNumber.Substring(3);
            }

            if (phoneNumber.StartsWith("+"))
            {
                phoneNumber = phoneNumber.Substring(1);
            }

            return phoneNumber;
        }
    }
}
