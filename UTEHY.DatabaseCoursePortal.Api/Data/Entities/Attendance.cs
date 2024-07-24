using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class Attendance : EntityBase
    {
        public int Id { get; set; }
        public string? StudentId { get; set; }
        public int ScheduleId { get; set; }
        public string? Note { get; set; }
        public bool Attendant { get; set; }
        public bool PermittedLeave { get; set; }
        public bool UnpermittedLeave { get; set; }
    }
}
