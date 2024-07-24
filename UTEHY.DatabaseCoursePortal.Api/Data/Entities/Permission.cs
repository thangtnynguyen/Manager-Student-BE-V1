using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class Permission : EntityBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public int? ParentPermissionId { get; set;}

        public string? Description { get; set; }

        public Permission? ParentPermission { get; set; }

        public virtual List<RolePermission> RolePermissions { get; set; }
    }
}
