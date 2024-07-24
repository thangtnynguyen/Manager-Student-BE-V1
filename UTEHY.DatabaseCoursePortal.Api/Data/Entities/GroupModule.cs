using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class GroupModule : EntityBase
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? InvitationCode { get; set; }

        public DateTime? ExpiryTimeInvitation { get; set; }

        public int? Year { get; set; }

        public int? Semester { get; set; }

        public bool? Status { get; set; }

        public int? TeacherId { get; set; }

        public virtual List<StudentGroupModule> StudentGroupModules { get; set; }

        public virtual List<ExamGroupModule> ExamGroupModules { get; set; }

        public virtual List<NotificationGroupModule> NotificationGroupModules { get; set; }

        public virtual Teacher Teacher { get; set; }

        public virtual List<Schedule> Schedules { get; set; }
    }
}
