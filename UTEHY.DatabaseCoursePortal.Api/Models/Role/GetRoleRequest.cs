using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Role
{
    public class GetRoleRequest: PagingRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
