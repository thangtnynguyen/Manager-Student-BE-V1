using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Permisson;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Role
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<PermissionDto> Permissions { get; set; }

    }
}
