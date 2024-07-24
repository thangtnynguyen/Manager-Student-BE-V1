using System.ComponentModel.DataAnnotations;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Account
{
    public class LoginEmailRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
