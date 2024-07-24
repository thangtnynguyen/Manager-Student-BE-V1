using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class Group : EntityBase
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public int? Type { get; set; }

        public string? Description { get; set; }

        public virtual List<UserGroup> UserGroups { get; set; }
    }
}
