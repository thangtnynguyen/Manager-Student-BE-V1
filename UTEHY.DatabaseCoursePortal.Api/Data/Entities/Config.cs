using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class Config : EntityBase
    {
        public int? Id { get; set; }

        public string? Key { get; set; }

        public string? Value { get; set; }

        public string? Description { get; set; }
    }
}
