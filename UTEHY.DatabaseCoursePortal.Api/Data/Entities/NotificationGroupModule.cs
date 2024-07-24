namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class NotificationGroupModule
    {
        public int? Id { get; set; }

        public int? GroupModuleId { get; set; }

        public int? NotificationId { get; set; }

        public virtual Notification Notification { get; set; }

        public virtual GroupModule GroupModule { get; set; }

    }
}
