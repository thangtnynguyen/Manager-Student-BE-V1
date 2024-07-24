using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Permisson
{
    public class PermissionDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public int? ParentPermissionId { get; set; }

        public string Description { get; set; }

        public List<PermissionDto> Children { get; set; }
    }
}
