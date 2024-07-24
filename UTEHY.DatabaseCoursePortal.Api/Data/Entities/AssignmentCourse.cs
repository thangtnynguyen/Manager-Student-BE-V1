using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class AssignmentCourse : EntityBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
    }
}
