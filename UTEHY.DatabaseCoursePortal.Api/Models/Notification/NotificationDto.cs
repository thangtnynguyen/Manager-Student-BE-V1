using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.GroupModule;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Notification
{
    public class NotificationDto
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        public string? Message { get; set; }

        public  List<GroupModuleDto> GroupModules { get; set; }
    }
}
