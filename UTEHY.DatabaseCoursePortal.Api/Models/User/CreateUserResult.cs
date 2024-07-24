using Microsoft.AspNetCore.Identity;

namespace UTEHY.DatabaseCoursePortal.Api.Models.User
{
    public class CreateUserResult
    {
        public IdentityResult? AddUserResult { get; set; }

        public IdentityResult? AddRoleResult { get; set; }

    }
}
