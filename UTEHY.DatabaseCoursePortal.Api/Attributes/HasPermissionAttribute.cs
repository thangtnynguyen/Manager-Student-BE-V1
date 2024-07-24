using Microsoft.AspNetCore.Authorization;

namespace UTEHY.DatabaseCoursePortal.Api.Attributes
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission): base(policy: permission)
        {
        }
    }
}
