using Microsoft.AspNetCore.Identity;

namespace UTEHY.DatabaseCoursePortal.Api.Models.User
{
    public class UserDto : IdentityUser<Guid>
    {
        public string? Name { get; set; }

        public string? AvatarUrl { get; set; }

        public List<string> Permissions { get; set; }

        public List<int>? GroupModuleIds { get; set; }



    }
}
