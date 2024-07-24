using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class Schedule : EntityBase
    {
        public int Id { get; set; }
        public int GroupModuleId { get; set; }
        public string? ClassPeriods { get; set; }
        public DateTime DateSchool { get; set; }
        public string? ClassRoom { get; set; }

        public virtual List<Attendance> Attendances { get; set; }
    }
}
