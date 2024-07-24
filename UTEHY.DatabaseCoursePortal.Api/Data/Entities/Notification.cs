using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class Notification : EntityBase
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        public string? Message { get; set; }

        public virtual List<NotificationGroupModule> NotificationGroupModules { get; set; }

    }
}
