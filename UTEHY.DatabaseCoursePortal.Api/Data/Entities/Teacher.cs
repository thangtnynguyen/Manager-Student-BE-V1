using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class Teacher : EntityBase
    {
        public int? Id { get; set; }

        public Guid? UserId { get; set; }

        public string? TeacherId { get; set; }

        public virtual User User { get; set; }

        public virtual List<Schedule> Schedules { get; set; }
    }
}
