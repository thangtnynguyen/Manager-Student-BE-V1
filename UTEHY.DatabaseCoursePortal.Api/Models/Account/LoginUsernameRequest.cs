namespace UTEHY.DatabaseCoursePortal.Api.Models.Account
{
    public class LoginUsernameRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public int Type { get; set; }
    }
}
